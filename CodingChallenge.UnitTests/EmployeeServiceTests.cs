using AutoMapper;
using Moq;
using CodingChallenge.Application.Dtos;
using CodingChallenge.Domain.Entities;
using CodingChallenge.Application.Services;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.CustomExceptions;
using System.Linq;

namespace CodingChallenge.UnitTests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IRepository<Employee>> _employeeRepositoryMock;
        private readonly Mock<IRepository<Device>> _deviceRepositoryMock;
        private readonly Mock<IRepository<EmployeeDevice>> _employeeDeviceRepositoryMock;
        private readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IEmployeeDeviceService> _employeeDeviceService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IRepository<Employee>>();
            _deviceRepositoryMock = new Mock<IRepository<Device>>();
            _employeeDeviceRepositoryMock = new Mock<IRepository<EmployeeDevice>>();
            _mapper = new Mock<IMapper>().Object;
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _employeeDeviceService = new Mock<IEmployeeDeviceService>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
                cfg.CreateMap<Device, DeviceDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();



            var employeeDeviceDto1 = new EmployeeDeviceDto { EmployeeId = 1, DeviceId = 1, DeviceType = "Device1" };
            var employeeDeviceDto2 = new EmployeeDeviceDto { EmployeeId = 1, DeviceId = 2, DeviceType = "Device2" };

            // Arrange
            _employeeDeviceService.Setup(service => service.GetEmployeeDevice(It.IsAny<List<int>>())).Returns(new List<EmployeeDeviceDto> { employeeDeviceDto1, employeeDeviceDto2 });

            _employeeService = new EmployeeService(
                _employeeRepositoryMock.Object,
                _employeeDeviceRepositoryMock.Object,
                _deviceRepositoryMock.Object,
                _employeeDeviceService.Object,
                _mapper, _unitOfWork);
        }

        [Fact]
        public void GetEmployee_ReturnsPaginatedEmployeeList()
        {
            // Arrange
            var employeeDto = new EmployeeDto
            {
                Name = "",
                Email = "",
                PageNumber = 1,
                PageSize = 10
            };

            var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John", Email = "john@example.com" },
            new Employee { Id = 2, Name = "Alice", Email = "alice@example.com" },
        };
            var pagedList = new Pagination<Employee>
            {
                PaginatedList = employees,
                TotalCount = employees.Count,
                TotalPages = 1
            };
            var devices = new List<Device>
            {
                new Device { Id = 1, Type = "Device1", Description = "Description1" },
                new Device { Id = 2, Type = "Device2", Description = "Description2" },
            };
            var devicePagedList = new Pagination<Device>
            {
                PaginatedList = devices,
                TotalCount = devices.Count,
                TotalPages = 1
            };
            _employeeRepositoryMock.Setup(repo => repo.GetQueryable(false)).Returns(employees.AsQueryable());
            _employeeRepositoryMock.Setup(repo => repo.GetPagedList(It.IsAny<IQueryable<Employee>>(), 1, 10))
                 .Returns(pagedList);

            _deviceRepositoryMock.Setup(repo => repo.GetQueryable(false)).Returns(devices.AsQueryable());
            _deviceRepositoryMock.Setup(repo => repo.GetPagedList(It.IsAny<IQueryable<Device>>(), 1, 10))
                .Returns(devicePagedList);
            // Act
            var result = _employeeService.GetEmployee(employeeDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.PaginatedList.Count); // Expected only John
            Assert.Equal(2, result.TotalCount);
        }

        [Fact]
        public void AddEmployee_ShouldAddEmployeeWithValidData()
        {
            // Arrange
            var employeeDto = new EmployeeDto
            {
                Name = "Philp Stan",
                Email = "philip@example.com",
            };
            var employee = new Employee
            {
                Name = "Philp Stan",
                Email = "philip@example.com",
            };
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John", Email = "john@example.com" },
                new Employee { Id = 2, Name = "Alice", Email = "alice@example.com" },
            };
            var devices = new List<Device>
            {
                new Device { Id = 1, Type = "Device1", Description = "Description1" },
                new Device { Id = 2, Type = "Device2", Description = "Description2" },
            };
            var employeeDevices = new List<EmployeeDevice> {
                new EmployeeDevice{Id=1 ,EmployeeId=1,DeviceId=1},
                new EmployeeDevice{Id=2 ,EmployeeId=1,DeviceId=2},
            };
            // Configure your mock repositories to return data as needed for this test.
            _employeeRepositoryMock.Setup(repo => repo.GetQueryable(false)).Returns(employees.AsQueryable);
            _deviceRepositoryMock.Setup(repo => repo.GetQueryable(false)).Returns(devices.AsQueryable());
            _employeeDeviceRepositoryMock.Setup(repo => repo.GetQueryable(false)).Returns(employeeDevices.AsQueryable());
            _employeeRepositoryMock.Setup(r => r.Add(It.IsAny<Employee>()))
               .Returns(employee);
            // Act
            var result = _employeeService.AddEmployee(employeeDto);

            // Assert
            // Verify that the result matches your expectations based on the input data and mocked repositories.
            // You can use Assert statements to check if the result is correct.
            Assert.NotNull(result);
            // Add more assertions as needed.
        }     
    }
}
