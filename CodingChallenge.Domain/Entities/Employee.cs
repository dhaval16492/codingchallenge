using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Domain.Entities
{
    /// <summary>
    /// Represents an entity for an employee in the application.
    /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Navigation property for EmployeeDevices, representing the relationship between employees and devices.
        /// </summary>
        public virtual ICollection<EmployeeDevice> EmployeeDevices { get; set; }
    }
}
