using CodingChallenge.Application.Dtos;
using CodingChallenge.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodingChallenge.Infra.Repository
{
    /// <summary>
    /// Generic repository implementation for CRUD operations using Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity managed by this repository.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Member Variables
        private readonly DbContext _context;
        protected internal DbSet<TEntity> _dbset;
        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context associated with this repository.</param>
        public Repository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }
        #endregion Constructor

        #region Protected Members
        /// <summary>
        /// Gets a queryable representation of the entity table with change tracking.
        /// </summary>
        protected IQueryable<TEntity> Table
        {
            get
            {
                return _dbset;
            }
        }

        /// <summary>
        /// Gets a queryable representation of the entity table without change tracking.
        /// </summary>
        protected IQueryable<TEntity> TableWithNoTracking
        {
            get
            {
                return _dbset.AsNoTracking();
            }
        }
        #endregion Protected Members

        #region Public Methods
        /// <summary>
        /// Gets a queryable collection of TEntity objects with optional change tracking.
        /// </summary>
        /// <param name="isTrackingRequired">Flag indicating whether change tracking is required (default is true).</param>
        /// <returns>A queryable collection of TEntity objects.</returns>
        public IQueryable<TEntity> GetQueryable(bool isTrackingRequired = true)
        {
            return isTrackingRequired ? Table : TableWithNoTracking;
        }

        /// <summary>
        /// Gets a TEntity object by its unique identifier (e.g., primary key).
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The TEntity object with the specified identifier.</returns>
        public TEntity? GetById(int id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// Adds a single TEntity object to the repository.
        /// </summary>
        /// <param name="entity">The TEntity object to add.</param>
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return _dbset.Add(entity).Entity;
        }

        /// <summary>
        /// Adds a collection of TEntity objects to the repository.
        /// </summary>
        /// <param name="entities">The collection of TEntity objects to add.</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            _dbset.AddRange(entities);
        }

        /// <summary>
        /// Updates an existing TEntity object in the repository.
        /// </summary>
        /// <param name="entity">The TEntity object to update.</param>
        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return _dbset.Update(entity).Entity;
        }

        /// <summary>
        /// Updates a collection of TEntity objects in the repository.
        /// </summary>
        /// <param name="entities">The collection of TEntity objects to update.</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            _dbset.UpdateRange(entities);
        }

        /// <summary>
        /// Removes an existing TEntity object from the repository.
        /// </summary>
        /// <param name="entity">The TEntity object to remove.</param>
        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbset.Remove(entity);
        }

        /// <summary>
        /// Removes a collection of TEntity objects from the repository.
        /// </summary>
        /// <param name="entities">The collection of TEntity objects to remove.</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            _dbset.RemoveRange(entities);
        }

        /// <summary>
        /// Gets a paginated list of TEntity objects from a queryable source.
        /// Useful for implementing pagination in API endpoints.
        /// </summary>
        /// <param name="source">The queryable source of TEntity objects.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of TEntity objects.</returns>
        public Pagination<TEntity> GetPagedList(IQueryable<TEntity> source, int pageNumber, int pageSize)
        {
            int count = source.Count();

            int totalPages = 0;
            List<TEntity> items = new List<TEntity>();
            if (pageSize > 0)
            {
                items = source.Skip((pageNumber) * pageSize).Take(pageSize).ToList();
                totalPages = (int)Math.Ceiling(count / (double)pageSize);
            }
            else
            {
                items = source.ToList();
                totalPages = 1;
            }
            return new Pagination<TEntity>()
            {
                PaginatedList = items,
                TotalCount = count,
                TotalPages = totalPages

            };
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbset;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        #endregion Public methods
    }
}
