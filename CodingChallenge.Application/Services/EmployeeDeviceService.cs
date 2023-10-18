using AutoMapper;
using CodingChallenge.Application.Dtos;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Application.Services
{
    public class EmployeeDeviceService : IEmployeeDeviceService
    {
        #region Member Variables
        public readonly IRepository<EmployeeDevice> _employeeDeviceRepository;
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _uniUnitOfWork;
        #endregion Member Variables
        #region Constructor
        public EmployeeDeviceService(IRepository<EmployeeDevice> employeeDeviceRepository,
            IMapper mapper,
            IUnitOfWork uniUnitOfWork)
        {
            _employeeDeviceRepository = employeeDeviceRepository;
            _mapper = mapper;
            _uniUnitOfWork = uniUnitOfWork;
        }
        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Retrieves a list of employee device DTOs based on a list of employee IDs.
        /// </summary>
        /// <param name="employeeIdList">The list of employee IDs to retrieve devices for.</param>
        /// <returns>A list of employee device DTOs.</returns>
        public List<EmployeeDeviceDto> GetEmployeeDevice(List<int> employeeIdList)
        {
            var result = _employeeDeviceRepository.GetAllIncluding(x => x.Device)
                .Where(x => !x.IsDeleted && employeeIdList.Contains(x.EmployeeId))
                .Select(x => new EmployeeDeviceDto
                {
                    Id = x.Id,
                    DeviceId = x.DeviceId,
                    EmployeeId = x.EmployeeId,
                    DeviceType = x.Device.Type
                }).ToList();

            return result;
        }

        /// <summary>
        /// Adds or removes employee devices based on the provided employee device DTOs.
        /// </summary>
        /// <param name="employeeDeviceDto">The list of employee device DTOs to add or remove.</param>
        /// <returns>The list of updated employee device DTOs.</returns>
        public List<EmployeeDeviceDto> AddRemoveEmployeeDevice(List<EmployeeDeviceDto> employeeDeviceDto)
        {
            List<EmployeeDevice> existingDevices = _employeeDeviceRepository.GetQueryable(false)
                                         .Where(x => !x.IsDeleted && x.EmployeeId == employeeDeviceDto[0].EmployeeId).ToList();

            if (existingDevices.Count == 0)
            {
                List<EmployeeDevice> employeeDevices = new List<EmployeeDevice>();
                employeeDeviceDto.ForEach(x =>
                {
                    var empDevice = new EmployeeDevice()
                    {
                        Id = x.Id,
                        DeviceId = x.DeviceId,
                        EmployeeId = x.EmployeeId,
                    };
                    employeeDevices.Add(empDevice);
                });
                _employeeDeviceRepository.AddRange(employeeDevices);
                _uniUnitOfWork.Save();
                return employeeDeviceDto;
            }

            List<EmployeeDeviceDto> employeeDeviceToAdd = new List<EmployeeDeviceDto>();
            List<EmployeeDevice> employeeDeviceToDelete = new List<EmployeeDevice>();

            List<int> deviceIdList = existingDevices.Select(x => x.DeviceId).ToList();
            List<int> newDeviceIdList = employeeDeviceDto.Select(x => x.DeviceId).ToList();

            employeeDeviceToDelete = existingDevices.Where(x => !newDeviceIdList.Contains(x.DeviceId)).ToList();
            if (employeeDeviceToDelete.Count > 0)
            {
                employeeDeviceToDelete.ForEach(x =>
                {
                    x.IsDeleted = true;
                });
                _employeeDeviceRepository.UpdateRange(employeeDeviceToDelete);
            }
         
            employeeDeviceToAdd = employeeDeviceDto.Where(x => !deviceIdList.Contains(x.DeviceId)).ToList();

            List<EmployeeDevice> employeeDevices1 = new List<EmployeeDevice>();
            employeeDeviceToAdd.ForEach(x =>
            {
                var empDevice = new EmployeeDevice()
                {
                    Id = x.Id,
                    DeviceId = x.DeviceId,
                    EmployeeId = x.EmployeeId,
                };
                employeeDevices1.Add(empDevice);
            });
            if (employeeDevices1.Count > 0)
            {
                _employeeDeviceRepository.AddRange(employeeDevices1);
            }
            _uniUnitOfWork.Save();

            return employeeDeviceDto;
        }
        #endregion Public Methods

    }
}
