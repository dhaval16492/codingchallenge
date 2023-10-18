using AutoMapper;
using Moq;
using CodingChallenge.Application.Dtos;
using CodingChallenge.Domain.Entities;
using CodingChallenge.Application.Services;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.CustomExceptions;

namespace CodingChallenge.UnitTests
{
    public class DeviceServiceTests
    {
        private readonly Mock<IRepository<Device>> _deviceRepositoryMock;
        private readonly Mock<IRepository<EmployeeDevice>> _employeeDeviceRepositoryMock;
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IDeviceService _deviceService;

        public DeviceServiceTests()
        {
            _deviceRepositoryMock = new Mock<IRepository<Device>>();
            _employeeDeviceRepositoryMock = new Mock<IRepository<EmployeeDevice>>();
            _mapper = new Mock<IMapper>().Object;
            _unitOfWork = new Mock<IUnitOfWork>().Object;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Device, DeviceDto>();
            });
            _mapper = mapperConfig.CreateMapper();

            // Arrange
            _deviceService = new DeviceService(_deviceRepositoryMock.Object,
                _employeeDeviceRepositoryMock.Object, _mapper, _unitOfWork);
        }

        [Fact]
        public void GetDevice_WithValidData_ReturnsPagedDeviceDtos()
        {


            var deviceDto = new DeviceDto
            {
                Type = "Smartphone",
                Description = "Mobile"
            };

            var devices = new List<Device>
            {
                new Device { Type = "Smartphone", Description = "Mobile Device 1" },
                new Device { Type = "Laptop", Description = "Laptop 1" }
            };

            var pagedList = new Pagination<Device>
            {
                PaginatedList = devices,
                TotalCount = devices.Count,
                TotalPages = 1
            };

            _deviceRepositoryMock.Setup(repo => repo.GetQueryable(false))
                .Returns(devices.AsQueryable());

            _deviceRepositoryMock.Setup(repo => repo.GetPagedList(It.IsAny<IQueryable<Device>>(), 1, 10))
                .Returns(pagedList);

            // Act
            var result = _deviceService.GetDevice(deviceDto);

            // Assert
            Assert.Equal(devices.Count, result.PaginatedList.Count);
            Assert.Equal(pagedList.TotalCount, result.TotalCount);
            Assert.Equal(pagedList.TotalPages, result.TotalPages);
        }

        [Fact]
        public void AddDevice_Success()
        {
            // Arrange
            var deviceDto = new DeviceDto { Type = "TestDevice", Description = "Test Description" };
            var device = new Device { Type = "TestDevice", Description = "Test Description" };

            _deviceRepositoryMock.Setup(r => r.GetQueryable(false))
                .Returns(new List<Device>().AsQueryable()); // No duplicates

            _deviceRepositoryMock.Setup(r => r.Add(It.IsAny<Device>()))
                .Returns(device);

            // Act
            var result = _deviceService.AddDevice(deviceDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(deviceDto.Type, result.Type);
            Assert.Equal(deviceDto.Description, result.Description);
        }

        [Fact]
        public void AddDevice_DuplicateDeviceType_ThrowsException()
        {
            // Arrange
            var deviceDto = new DeviceDto { Type = "TestDevice", Description = "Test Description" };
            var existingDevice = new Device { Type = "TestDevice", Description = "Existing Description" };

            _deviceRepositoryMock.Setup(r => r.GetQueryable(false))
                .Returns(new List<Device> { existingDevice }.AsQueryable()); // Duplicate device type

            // Act and Assert
            Assert.Throws<CustomException>(() => _deviceService.AddDevice(deviceDto));
        }

        [Fact]
        public void AddDevice_SuccessWithMock()
        {
            // Arrange
            var deviceDto = new DeviceDto { Type = "TestDevice", Description = "Test Description" };
            var device = new Device { Type = "TestDevice", Description = "Test Description" };

            _deviceRepositoryMock.Setup(r => r.GetQueryable(false))
                .Returns(new List<Device>().AsQueryable()); // No duplicates

            _deviceRepositoryMock.Setup(r => r.Add(It.IsAny<Device>()))
                .Returns(device);

            // Act
            var result = _deviceService.AddDevice(deviceDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(deviceDto.Type, result.Type);
            Assert.Equal(deviceDto.Description, result.Description);
         }

        [Fact]
        public void AddDevice_Exception_ThrowsCustomException()
        {
            // Arrange
            var deviceDto = new DeviceDto { Type = "TestDevice", Description = "Test Description" };

            _deviceRepositoryMock.Setup(r => r.GetQueryable(false))
                .Returns(new List<Device>().AsQueryable()); // No duplicates

            _deviceRepositoryMock.Setup(r => r.Add(It.IsAny<Device>()))
                .Throws(new Exception("Simulated Exception"));

            // Act and Assert
            var exception = Assert.Throws<CustomException>(() => _deviceService.AddDevice(deviceDto));
            Assert.Contains("Error adding device", exception.Message);
        }

        [Fact]
        public void UpdateDevice_WithValidData_ReturnsUpdatedDeviceDto()
        {
           
            var deviceDto = new DeviceDto
            {
                Id = 1,
                Type = "UpdatedType",
                Description = "UpdatedDescription",
            };

            // Mock the behavior of the dependencies
            _deviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new[]
                {
                new Device { Id = 1, Type = "OriginalType", Description = "OriginalDescription", IsDeleted = false },
                }.AsQueryable());

            // Mock the behavior of the repository update method
            _deviceRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<Device>()))
                .Returns((Device device) => device);           

            // Act
            var result = _deviceService.UpdateDevice(deviceDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("UpdatedType", result.Type);
            Assert.Equal("UpdatedDescription", result.Description);
        }

        [Fact]
        public void UpdateDevice_WithDuplicatedType_ThrowsCustomException()
        {
            
            var deviceDto = new DeviceDto
            {
                Id = 1,
                Type = "DuplicatedType",
                Description = "UpdatedDescription",
            };

            // Mock the behavior of the dependencies
            _deviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new[]
                {
                    new Device { Id = 1, Type = "OriginalType", Description = "OriginalDescription", IsDeleted = false },
                    new Device { Id = 2, Type = "DuplicatedType", Description = "DuplicatedDescription", IsDeleted = false },
                }.AsQueryable());

            // Act and Assert
            Assert.Throws<CustomException>(() => _deviceService.UpdateDevice(deviceDto));
        }

        [Fact]
        public void UpdateDevice_WithNonExistentDevice_ReturnsEmptyDeviceDto()
        {
            var deviceDto = new DeviceDto
            {
                Id = 2, // Device with ID 2 doesn't exist
                Type = "UpdatedType",
                Description = "UpdatedDescription",
            };

            // Mock the behavior of the dependencies
            _deviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new[]
                {
                new Device { Id = 1, Type = "OriginalType", Description = "OriginalDescription", IsDeleted = false },
                }.AsQueryable());

            // Act
            var result = _deviceService.UpdateDevice(deviceDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public void DeleteDevice_WithValidId_DeviceIsMarkedAsDeleted()
        {           

            var deviceId = 1;

            // Mock the behavior of the device repository to return a device
            _deviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new[]
                {
                new Device { Id = 1, IsDeleted = false },
                }.AsQueryable());

            // Mock the behavior of the employee device repository to return null
            _employeeDeviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new EmployeeDevice[0].AsQueryable());

            // Act
            _deviceService.DeleteDevice(deviceId);

            // Assert that the device is marked as deleted
            _deviceRepositoryMock.Verify(repo => repo.Update(It.IsAny<Device>()), Times.Once);
            
        }

        [Fact]
        public void DeleteDevice_DeviceLinkedWithEmployee_ThrowsCustomException()
        {
            
            var deviceId = 1;

            // Mock the behavior of the device repository to return a device
            _deviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new[]
                {
                new Device { Id = 1, IsDeleted = false },
                }.AsQueryable());

            // Mock the behavior of the employee device repository to return a linked employee device
            _employeeDeviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new[]
                {
                new EmployeeDevice { DeviceId = 1 },
                }.AsQueryable());

            // Act and Assert
            Assert.Throws<CustomException>(() => _deviceService.DeleteDevice(deviceId));
        }

        [Fact]
        public void DeleteDevice_NonExistentDevice_DoesNothing()
        {
              var deviceId = 2; // Device with ID 2 doesn't exist

            // Mock the behavior of the device repository to return no device
            _deviceRepositoryMock
                .Setup(repo => repo.GetQueryable(false))
                .Returns(new Device[0].AsQueryable());

            // Act
            _deviceService.DeleteDevice(deviceId);

            // Assert that nothing is updated or saved
            _deviceRepositoryMock.Verify(repo => repo.Update(It.IsAny<Device>()), Times.Never);
           
        }


    }
}