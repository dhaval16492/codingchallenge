namespace CodingChallenge.Application.Dtos
{
    /// <summary>
    /// Represents a pagination result for a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the paginated list.</typeparam>
    public class Pagination<T>
    {
        /// <summary>
        /// Gets or sets the total number of pages in the paginated list.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the total count of items across all pages.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the list of items for the current page.
        /// </summary>
        public List<T> PaginatedList { get; set; }
    }
}
