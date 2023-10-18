using System;

namespace CodingChallenge.Application.Dtos
{
    /// <summary>
    /// Data transfer object for representing a device.
    /// </summary>
    public class DeviceDto : RequestDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the device.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the type or category of the device.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the device.
        /// </summary>
        public string? Description { get; set; }
    }
}
