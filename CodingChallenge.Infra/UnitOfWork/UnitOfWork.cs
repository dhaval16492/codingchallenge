using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.Constants;
using CodingChallenge.Common.CustomExceptions;
using CodingChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace CodingChallenge.Infra.UnitOfWork
{
    /// <summary>
    /// Represents a unit of work for managing database transactions and changes.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Member Variables
        private readonly DbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDbContextTransaction? _dbContextTransaction;
        private bool disposed = false;
        #endregion Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The DbContext associated with the unit of work.</param>
        /// <param name="httpContextAccessor">The IHttpContextAccessor for accessing the current HTTP context.</param>
        public UnitOfWork(DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Constructors

        #region Public Methods
        /// <summary>
        /// Saves changes made within the unit of work.
        /// </summary>
        /// <returns>The number of entities saved to the database.</returns>
        public int Save()
        {
            int result = 0;
            try
            {
                SetAuditFields();
                result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return result;
        }

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (_dbContextTransaction == null)
            {
                _context.Database.BeginTransaction();
            }
        }

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        public void RollBackTransaction()
        {
            if (_dbContextTransaction == null)
            {
                _context.Database.RollbackTransaction();
            }
        }

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        public void Commit()
        {
            if (_dbContextTransaction == null)
            {
                _context.Database.CommitTransaction();
            }
        }

        /// <summary>
        /// Enables automatic change detection in the unit of work.
        /// </summary>
        public void EnableAutoDetectChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        /// <summary>
        /// Disables automatic change detection in the unit of work.
        /// </summary>
        public void DisableAutoDetectChanges()
        {
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }
        #endregion Public Methods

        #region Private Methods
        private static void HandleException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException concurrencyException)
            {
                throw new CustomException(Constants.Error.ERROR_CONCURRENCY, concurrencyException);
            }

            if (exception is DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException != null)
                {
                    if (dbUpdateException.InnerException is SqlException sqlException)
                    {
                        throw sqlException.Number switch
                        {
                            2627 => new CustomException("Record is not Unique.", sqlException),

                            547 => new CustomException("Constraint Check Violation.", sqlException),

                            2601 => new CustomException("Constraint Check Violation.", sqlException),

                            _ => sqlException,
                        };
                    }
                    throw dbUpdateException;
                }
            }
        }

        private void SetAuditFields()
        {
            var now = DateTime.UtcNow;
            foreach (var changedEntity in _context.ChangeTracker.Entries())
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = now;
                            entity.UpdatedDate = now;
                            break;
                        case EntityState.Modified:
                            entity.UpdatedDate = now;
                            break;
                    }
                }
            }
        }
        #endregion Private Methods

        #region IDisposable
        /// <summary>
        /// Disposes of the unit of work and associated resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes of the unit of work and associated resources.
        /// </summary>
        /// <param name="disposing">True if disposing of managed resources, false if not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContextTransaction?.Dispose();
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion IDisposable
    }
}
