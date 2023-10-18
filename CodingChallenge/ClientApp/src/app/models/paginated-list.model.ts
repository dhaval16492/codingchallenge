/**
 * Represents a paginated list of items, typically used for paginated data retrieval.
 * @template T The type of items in the list.
 */
export interface PaginatedList<T> {
  totalPages: number; // The total number of pages in the paginated data.
  totalCount: number; // The total count of items across all pages.
  paginatedList: T[]; // The list of items on the current page.
}
