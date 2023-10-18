using CodingChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infra.EntityMapping
{
    /// <summary>
    /// Configuration class for mapping the `EmployeeDevice` entity to the database using Entity Framework Core.
    /// </summary>
    public class EmployeeDeviceConfiguration : IEntityTypeConfiguration<EmployeeDevice>
    {
        /// <summary>
        /// Configures the entity properties and relationships for the `EmployeeDevice` entity.
        /// </summary>
        /// <param name="entity">The entity builder for configuring the `EmployeeDevice` entity.</param>
        public void Configure(EntityTypeBuilder<EmployeeDevice> entity)
        {
            // Configure primary key and property constraints for the EmployeeDevice entity
            entity.ToTable("employee_device");
            entity.HasKey(e => e.Id);

            // Configure auto-generation of the Id property upon entity insertion
            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.EmployeeId)
              .HasColumnName("employee_id");

            entity.Property(x => x.DeviceId)
              .HasColumnName("device_id");

            // Configure relationships between EmployeeDevice, Employee, and Device entities
            entity.HasOne(x => x.Employee)
                .WithMany(e => e.EmployeeDevices)
                .HasForeignKey(ed => ed.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(x => x.Device)
                .WithMany(e => e.EmployeeDevices)
                .HasForeignKey(ed => ed.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
