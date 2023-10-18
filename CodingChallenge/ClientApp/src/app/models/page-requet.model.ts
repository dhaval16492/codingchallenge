/**
 * Represents a page request for pagination.
 */
export interface PageRequest {
  pageSize?: number; // The number of items per page (optional)
  pageNumber?: number; // The current page number (optional)
}
