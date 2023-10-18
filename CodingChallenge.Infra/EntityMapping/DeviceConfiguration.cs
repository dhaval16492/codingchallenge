using CodingChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infra.EntityMapping
{
    /// <summary>
    /// Configuration class for mapping the `Device` entity to the database using Entity Framework Core.
    /// </summary>
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        /// <summary>
        /// Configures the entity properties and relationships for the `Device` entity.
        /// </summary>
        /// <param name="entity">The entity builder for configuring the `Device` entity.</param>
        public void Configure(EntityTypeBuilder<Device> entity)
        {
            // Configure primary key and property constraints for the Device entity
            entity.ToTable("device");
            entity.HasKey(e => e.Id);

            // Configure auto-generation of the Id property upon entity insertion
            
            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            // Configure constraints for the Type property
            entity.Property(e => e.Type)
                   .HasColumnName("type")
                   .IsRequired()
                   .HasMaxLength(50);

            // Configure constraints for the Description property
            entity.Property(e => e.Description)
                .HasColumnName("description")
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
