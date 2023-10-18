using System;
using System.Collections.Generic;

namespace CodingChallenge.Application.Dtos
{
    /// <summary>
    /// Data transfer object for representing employee information.
    /// </summary>
    public class EmployeeDto : BaseEntityDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets a list of devices assigned to the employee.
        /// </summary>
        public List<DeviceDto>? DeviceList { get; set; }
    }
}
