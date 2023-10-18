using AutoMapper;
using CodingChallenge.Application.Dtos;
using CodingChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore()) // Ignore PageNumber during mapping
                .ForMember(dest => dest.PageSize, opt => opt.Ignore()) // Ignore PageSize during mapping
                .ReverseMap(); // Create a reverse mapping configuration

            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore()) // Ignore PageNumber during mapping
                .ForMember(dest => dest.PageSize, opt => opt.Ignore()) // Ignore PageSize during mapping
                .ReverseMap(); // Create a reverse mapping configuration

            CreateMap<EmployeeDevice, EmployeeDeviceDto>()
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore()) // Ignore PageNumber during mapping
                .ForMember(dest => dest.PageSize, opt => opt.Ignore()) // Ignore PageSize during mapping
                .ReverseMap(); // Create a reverse mapping configuration
        }
    }
}
