﻿using CodingChallenge.Domain.Entities;
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
    /// Configuration class for mapping the `Employee` entity to the database using Entity Framework Core.
    /// </summary>
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        /// <summary>
        /// Configures the entity properties and relationships for the `Employee` entity.
        /// </summary>
        /// <param name="entity">The entity builder for configuring the `Employee` entity.</param>
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            // Configure primary key and property constraints for the Employee entity
            entity.ToTable("employee");
            entity.HasKey(e => e.Id);

            // Configure auto-generation of the Id property upon entity insertion
            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            // Configure constraints for the Name property
            entity.Property(e => e.Name)
                .HasColumnName("name")
                   .IsRequired()
                   .HasMaxLength(100);

            // Configure constraints for the Email property
            entity.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
