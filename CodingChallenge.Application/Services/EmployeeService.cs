using AutoMapper;
using CodingChallenge.Application.Dtos;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.CustomExceptions;
using CodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Member Variables
        public readonly IRepository<Employee> _employeeRepository;
        public readonly IRepository<EmployeeDevice> _employeeDeviceRepository;
        public readonly IRepository<Device> _deviceRepository;
        public readonly IEmployeeDeviceService _employeeDeviceService;
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _uniUnitOfWork;
        #endregion Member Variables

        #region Constructor

        public EmployeeService(IRepository<Employee> employeeRepository,
            IRepository<EmployeeDevice> employeeDeviceRepository,
            IRepository<Device> deviceRepository,
            IEmployeeDeviceService employeeDeviceService,
            IMapper mapper,
            IUnitOfWork uniUnitOfWork)
        {
            _employeeRepository = employeeRepository;
            _employeeDeviceRepository = employeeDeviceRepository;
            _deviceRepository = deviceRepository;
            _employeeDeviceService = employeeDeviceService;
            _mapper = mapper;
            _uniUnitOfWork = uniUnitOfWork;
        }
        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Retrieves a paginated list of employee DTOs based on filter criteria.
        /// </summary>
        /// <param name="employeeDto">The filter criteria for employee retrieval.</param>
        /// <returns>A paginated list of employee DTOs.</returns>
        public Pagination<EmployeeDto> GetEmployee(EmployeeDto employeeDto)
        {
            var result = _employeeRepository.GetQueryable(false).Where(x => !x.IsDeleted)
                .Where(x => !string.IsNullOrWhiteSpace(employeeDto.Name) ? x.Name.Contains(employeeDto.Name) : true)
                .Where(x => !string.IsNullOrWhiteSpace(employeeDto.Email) ? x.Email.Contains(employeeDto.Email) : true);

            Pagination<Employee> pagedList = _employeeRepository.GetPagedList(result, employeeDto.PageNumber ?? 0, employeeDto.PageSize ?? 0);
            List<EmployeeDto> employeeList = _mapper.Map<List<EmployeeDto>>(pagedList.PaginatedList);
            List<int> employeeIdList = pagedList.PaginatedList.Select(x => x.Id).ToList();
            var employeeDeviceList = _employeeDeviceRepository.GetQueryable(false)
                .Where(x => employeeIdList.Contains(x.EmployeeId));


            var assignedDevices = _employeeDeviceService.GetEmployeeDevice(employeeIdList);
            employeeList.ForEach((EmployeeDto employee) =>
            {
                employee.DeviceList = assignedDevices.Where(x => x.EmployeeId == employee.Id)
                                .Select(x => new DeviceDto
                                {
                                    Id = x.DeviceId,
                                    Type = x.DeviceType
                                }).ToList();
            });

            return new Pagination<EmployeeDto>()
            {
                PaginatedList = employeeList,
                TotalCount = pagedList.TotalCount,
                TotalPages = pagedList.TotalPages
            };
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>An employee DTO with the specified ID.</returns>
        public EmployeeDto GetEmployeeById(int id)
        {
            Employee employee = _employeeRepository.GetById(id);

            return _mapper.Map<EmployeeDto>(employee);

        }

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        /// <param name="employeeDto">The employee DTO to add.</param>
        /// <returns>The added employee DTO.</returns>
        public EmployeeDto AddEmployee(EmployeeDto employeeDto)
        {
            int duplicatedEmailCount = _employeeRepository.GetQueryable(false)
                .Count(x => x.Email.Equals(employeeDto.Email) && !x.IsDeleted);
            if (duplicatedEmailCount > 0)
            {
                throw new CustomException("Email Id already exists.");
            }
            Employee employee = new Employee()
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
            };


            var result = _employeeRepository.Add(employee);

            _uniUnitOfWork.Save();
            List<EmployeeDeviceDto> employeeDeviceDto = new List<EmployeeDeviceDto>();
            employeeDto?.DeviceList?.ForEach(x =>
            {
                var emp = new EmployeeDeviceDto()
                {
                    EmployeeId = employee.Id,
                    DeviceId = x.Id ?? 0
                };
                employeeDeviceDto.Add(emp);
            });
            var employeeDetailDto = _mapper.Map<EmployeeDto>(result);
            if (employeeDeviceDto.Count() > 0)
            {
                var employeeDevices = _employeeDeviceService.AddRemoveEmployeeDevice(employeeDeviceDto);
                List<int> deviceIdList = employeeDevices.Select(x => x.DeviceId).Distinct().ToList();
                List<Device> deviceList = _deviceRepository.GetQueryable(false)
                    .Where(x => deviceIdList.Contains(x.Id)).ToList();
                List<DeviceDto> deviceDtoList = _mapper.Map<List<DeviceDto>>(deviceList);

                employeeDetailDto.DeviceList = deviceDtoList;
            }
            return employeeDetailDto;

        }

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="employeeDto">The employee DTO with updated information.</param>
        /// <returns>The updated employee DTO.</returns>
        public EmployeeDto UpdateEmployee(EmployeeDto employeeDto)
        {
            int duplicatedEmailCount = _employeeRepository.GetQueryable(false)
                .Count(x => x.Id != employeeDto.Id
                && x.Email.Equals(employeeDto.Email) && !x.IsDeleted);
            if (duplicatedEmailCount > 0)
            {
                throw new CustomException("Email Id already exists.");
            }
            Employee employee = _employeeRepository.GetQueryable(false).FirstOrDefault(x => x.Id == employeeDto.Id);
            if (employee != null)
            {
                employee.Name = employeeDto.Name;
                employee.Email = employeeDto.Email;
                var result = _employeeRepository.Update(employee);
                _uniUnitOfWork.Save();
                var employeeDetailDto = _mapper.Map<EmployeeDto>(result);
                List<EmployeeDeviceDto> employeeDeviceDto = new List<EmployeeDeviceDto>();

                var deviceDtoList = employeeDto?.DeviceList;

                deviceDtoList?.ForEach(x =>
                {
                    var emp = new EmployeeDeviceDto()
                    {
                        EmployeeId = employee.Id,
                        DeviceId = x.Id ?? 0
                    };
                    employeeDeviceDto.Add(emp);
                });
                if (employeeDeviceDto.Count > 0)
                {
                    _employeeDeviceService.AddRemoveEmployeeDevice(employeeDeviceDto);
                }
                employeeDetailDto.DeviceList = deviceDtoList;
                return employeeDetailDto;
            }
            return new EmployeeDto();
        }

        /// <summary>
        /// Marks an employee as deleted and handles employee device deletions.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        public void DeleteEmployee(int id)
        {
            Employee employee = _employeeRepository.GetQueryable(false).FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                _employeeRepository.Update(employee);
                List<EmployeeDevice> employeeDeviceList = _employeeDeviceRepository.GetQueryable(false)
                    .Where(x => x.EmployeeId == employee.Id).ToList();

                foreach (var employeeDevice in employeeDeviceList)
                {
                    employeeDevice.IsDeleted = true;
                }

                _employeeDeviceRepository.UpdateRange(employeeDeviceList);

                _uniUnitOfWork.Save();
            }
        }
        #endregion Public Methods
    }
}
