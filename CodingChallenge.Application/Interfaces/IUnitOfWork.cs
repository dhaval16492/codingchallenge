namespace CodingChallenge.Application.Interfaces
{
    /// <summary>
    /// Represents a Unit of Work interface for managing database transactions and changes.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes made within the unit of work.
        /// </summary>
        /// <returns>The number of entities saved to the database.</returns>
        int Save();

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        void RollBackTransaction();

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Enables automatic change detection in the unit of work.
        /// </summary>
        void EnableAutoDetectChanges();

        /// <summary>
        /// Disables automatic change detection in the unit of work.
        /// </summary>
        void DisableAutoDetectChanges();
    }
}
