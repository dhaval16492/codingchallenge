using System;

namespace CodingChallenge.Application.Dtos
{
    /// <summary>
    /// Data transfer object for specifying pagination options in requests.
    /// </summary>
    public class RequestDto
    {
        /// <summary>
        /// Gets or sets the maximum number of items to display on a page.
        /// </summary>
        public int? PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the current page number to retrieve.
        /// </summary>
        public int? PageNumber { get; set; } = 1;
    }
}
