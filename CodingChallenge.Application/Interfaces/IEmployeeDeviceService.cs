using CodingChallenge.Application.Dtos;
using System.Collections.Generic;

namespace CodingChallenge.Application.Interfaces
{
    /// <summary>
    /// Interface defining the contract for employee device-related service operations.
    /// </summary>
    public interface IEmployeeDeviceService
    {
        /// <summary>
        /// Retrieves a list of employee devices based on a list of employee IDs.
        /// </summary>
        /// <param name="employeeIdList">A list of employee IDs for which devices are to be retrieved.</param>
        /// <returns>A list of employee devices in the form of EmployeeDeviceDto objects.</returns>
        List<EmployeeDeviceDto> GetEmployeeDevice(List<int> employeeIdList);

        /// <summary>
        /// Adds or removes employee devices based on the provided data.
        /// </summary>
        /// <param name="employeeDeviceDto">A list of employee device data transfer objects indicating devices to be added or removed.</param>
        /// <returns>A list of updated employee devices in the form of EmployeeDeviceDto objects.</returns>
        List<EmployeeDeviceDto> AddRemoveEmployeeDevice(List<EmployeeDeviceDto> employeeDeviceDto);
    }
}
