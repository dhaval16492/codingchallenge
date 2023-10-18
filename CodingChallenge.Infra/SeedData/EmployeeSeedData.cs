using CodingChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infra.SeedData
{
    public class EmployeeSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                Name = "Paul Walker",
                Email = "paul@gmail.com",

            },
            new Employee
            {
                Id = 2,
                Name = "Chris Evans",
                Email = "chris@gmail.com",

            });

            modelBuilder.Entity<EmployeeDevice>().HasData(

                    new EmployeeDevice
                    {
                        Id = 1,
                        EmployeeId = 1,
                        DeviceId = 1,
                    },
                    new EmployeeDevice
                    {
                        Id = 2,
                        EmployeeId = 1,
                        DeviceId = 2,
                    },
                    new EmployeeDevice
                    {
                        Id = 3,
                        EmployeeId = 1,
                        DeviceId = 3,
                    },
                     new EmployeeDevice
                     {
                         Id = 4,
                         EmployeeId = 2,
                         DeviceId = 3,
                     },
                    new EmployeeDevice
                    {
                        Id = 5,
                        EmployeeId = 2,
                        DeviceId = 4,
                    },
                    new EmployeeDevice
                    {
                        Id = 6,
                        EmployeeId = 2,
                        DeviceId = 5,
                    }
            );
        }
    }
}
