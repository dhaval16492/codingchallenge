using AutoMapper;
using CodingChallenge.Application.Dtos;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.CustomExceptions;
using CodingChallenge.Domain.Entities;

namespace CodingChallenge.Application.Services
{
    public class DeviceService : IDeviceService
    {
        #region Member Variables
        public readonly IRepository<Device> _deviceRepository;
        public readonly IRepository<EmployeeDevice> _employeeDeviceRepository;
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _uniUnitOfWork;
        #endregion Member Variables

        #region Constructor
        public DeviceService(IRepository<Device> deviceRepository,
            IRepository<EmployeeDevice> employeeDeviceRepository,
            IMapper mapper,
            IUnitOfWork uniUnitOfWork)
        {
            _deviceRepository = deviceRepository;
            _employeeDeviceRepository = employeeDeviceRepository;
            _mapper = mapper;
            _uniUnitOfWork = uniUnitOfWork;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Retrieves a paginated list of devices based on the specified criteria.
        /// </summary>
        /// <param name="deviceDto">The criteria for device retrieval.</param>
        /// <returns>A paginated list of device DTOs.</returns>
        public Pagination<DeviceDto> GetDevice(DeviceDto deviceDto)
        {
            var result = _deviceRepository.GetQueryable(false).Where(x => !x.IsDeleted)
                .Where(x => !string.IsNullOrWhiteSpace(deviceDto.Type) ? x.Type.Contains(deviceDto.Type) : true)
                .Where(x => !string.IsNullOrWhiteSpace(deviceDto.Description) ? x.Description.Contains(deviceDto.Description) : true);

            Pagination<Device> pagedList = _deviceRepository.GetPagedList(result, deviceDto.PageNumber ?? 0, deviceDto.PageSize ?? 0);

            return new Pagination<DeviceDto>()
            {
                PaginatedList = _mapper.Map<List<DeviceDto>>(pagedList.PaginatedList),
                TotalCount = pagedList.TotalCount,
                TotalPages = pagedList.TotalPages
            };
        }

        /// <summary>
        /// Adds a new device based on the provided device DTO.
        /// </summary>
        /// <param name="deviceDto">The device DTO to add.</param>
        /// <returns>The added device DTO.</returns>
        public DeviceDto AddDevice(DeviceDto deviceDto)
        {
            if (deviceDto == null)
            {
                throw new ArgumentNullException("deviceDto");
            }

            int duplicatedDeviceCount = _deviceRepository.GetQueryable(false)
                .Count(x => x.Type.Equals(deviceDto.Type) && !x.IsDeleted);
            if (duplicatedDeviceCount > 0)
            {
                throw new CustomException("Device Type already exists.");
            }
            try
            {
                Device device = new Device()
                {
                    Type = deviceDto.Type,
                    Description = deviceDto.Description,
                };
                var result = _deviceRepository.Add(device);
                _uniUnitOfWork.Save();
                return _mapper.Map<DeviceDto>(result);
            }
            catch (Exception ex)
            {

                throw new CustomException("Error adding device", ex);
            }
        }

        /// <summary>
        /// Updates an existing device based on the provided device DTO.
        /// </summary>
        /// <param name="deviceDto">The device DTO with updated information.</param>
        /// <returns>The updated device DTO.</returns>
        public DeviceDto UpdateDevice(DeviceDto deviceDto)
        {
            int duplicatedDeviceCount = _deviceRepository.GetQueryable(false)
                .Count(x => x.Id != deviceDto.Id && !x.IsDeleted
                && x.Type.Equals(deviceDto.Type));
            if (duplicatedDeviceCount > 0)
            {
                throw new CustomException("Device Type already exists.");
            }
            Device device = _deviceRepository.GetQueryable(false).FirstOrDefault(x => x.Id == deviceDto.Id);
            if (device != null)
            {
                device.Type = deviceDto.Type;
                device.Description = deviceDto.Description;
                var result = _deviceRepository.Update(device);
                _uniUnitOfWork.Save();
                return _mapper.Map<DeviceDto>(result);
            }

            return new DeviceDto() {Id=0,Type=string.Empty,Description=string.Empty };
        }

        /// <summary>
        /// Deletes a device by its ID.
        /// </summary>
        /// <param name="id">The ID of the device to delete.</param>
        public void DeleteDevice(int id)
        {
            Device device = _deviceRepository.GetQueryable(false).FirstOrDefault(x => x.Id == id);
            if (device != null)
            {
                EmployeeDevice employeeDevice = _employeeDeviceRepository.GetQueryable(false).FirstOrDefault(x => x.DeviceId == id);
                if (employeeDevice != null)
                {
                    throw new CustomException("Device is linked with Employee.");
                }
                device.IsDeleted = true;
                _deviceRepository.Update(device);
                _uniUnitOfWork.Save();
            }
        }
        #endregion Public Methods
    }
}
