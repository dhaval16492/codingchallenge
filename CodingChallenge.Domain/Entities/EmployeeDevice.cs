using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Domain.Entities
{
    /// <summary>
    /// Represents an entity for the assignment of devices to employees in the application.
    /// </summary>
    public class EmployeeDevice : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee device assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated employee.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated device.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// Navigation property for the associated employee entity.
        /// </summary>
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Navigation property for the associated device entity.
        /// </summary>
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }
    }
}
