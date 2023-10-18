using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Domain.Entities
{
    /// <summary>
    /// Represents an entity for a device in the application.
    /// </summary>
    public class Device : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the device.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the device.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the device.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Navigation property for EmployeeDevices, representing the relationship between devices and employees.
        /// </summary>
        public virtual ICollection<EmployeeDevice> EmployeeDevices { get; set; }
    }
}
