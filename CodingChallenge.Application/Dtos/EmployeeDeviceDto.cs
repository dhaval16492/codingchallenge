using System;

namespace CodingChallenge.Application.Dtos
{
    /// <summary>
    /// Data transfer object for representing the relationship between employees and devices.
    /// </summary>
    public class EmployeeDeviceDto : BaseEntityDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee-device relationship.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with the device.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the device associated with the employee.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the type or category of the associated device.
        /// </summary>
        public string DeviceType { get; set; }
    }
}
