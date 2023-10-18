using CodingChallenge.Common.Constants;
using CodingChallenge.Infra.EntityMapping;
using CodingChallenge.Infra.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Metadata;

namespace CodingChallenge.Infra
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the application.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        public AppDbContext()
        {
            // Empty constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with provided options.
        /// </summary>
        /// <param name="options">The DbContext options to configure the context.</param>
        public AppDbContext(DbContextOptions options) : base(options)
        {
            // Constructor with DbContextOptions
        }

        /// <summary>
        /// Configures database options and behaviors for the DbContext.
        /// </summary>
        /// <param name="optionsBuilder">The DbContextOptionsBuilder used to configure options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable lazy loading proxies for related entities
            optionsBuilder.UseLazyLoadingProxies();

            // Ignore the warning related to detached lazy loading
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        }

        /// <summary>
        /// Configures the database model and relationships for the DbContext.
        /// </summary>
        /// <param name="modelBuilder">The ModelBuilder instance used for configuring the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Constants.DbSchema.Name);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeDeviceConfiguration());

            DeviceSeedData.Seed(modelBuilder);
            EmployeeSeedData.Seed(modelBuilder);
        }
    }
}
