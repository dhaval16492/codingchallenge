using CodingChallenge.Application.Dtos;
using System.Linq.Expressions;

namespace CodingChallenge.Application.Interfaces
{
    /// <summary>
    /// Represents a generic repository interface for data access in a .NET application.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository operates on.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets a queryable collection of TEntity objects with optional tracking.
        /// </summary>
        /// <param name="isTrackingRequired">Flag indicating whether change tracking is required (default is true).</param>
        /// <returns>A queryable collection of TEntity objects.</returns>
        IQueryable<TEntity> GetQueryable(bool isTrackingRequired = true);

        /// <summary>
        /// Gets a TEntity object by its unique identifier (e.g., primary key).
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The TEntity object with the specified identifier.</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Adds a single TEntity object to the repository.
        /// </summary>
        /// <param name="entity">The TEntity object to add.</param>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds a collection of TEntity objects to the repository.
        /// </summary>
        /// <param name="entities">The collection of TEntity objects to add.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates an existing TEntity object in the repository.
        /// </summary>
        /// <param name="entity">The TEntity object to update.</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Updates a collection of TEntity objects in the repository.
        /// </summary>
        /// <param name="entities">The collection of TEntity objects to update.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes an existing TEntity object from the repository.
        /// </summary>
        /// <param name="entity">The TEntity object to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes a collection of TEntity objects from the repository.
        /// </summary>
        /// <param name="entities">The collection of TEntity objects to remove.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Gets a paginated list of TEntity objects from a queryable source.
        /// Useful for implementing pagination in API endpoints.
        /// </summary>
        /// <param name="source">The queryable source of TEntity objects.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of TEntity objects.</returns>
        Pagination<TEntity> GetPagedList(IQueryable<TEntity> source, int pageNumber, int pageSize);

        /// <summary>
        /// Gets a queryable collection of TEntity objects with optional inclusion of related entities.
        /// </summary>
        /// <param name="includeProperties">An array of expressions specifying related entities to include.</param>
        /// <returns>A queryable collection of TEntity objects with related entities included.</returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
