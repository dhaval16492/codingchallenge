using CodingChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infra.SeedData
{
    public class DeviceSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasData(
            new Device
            {
                Id = 1,
                Type = "Smartphone",
                Description = "A mobile phone with advanced features, often including internet access and app capabilities."
            },
            new Device
            {
                Id = 2,
                Type = "Laptop",
                Description = "A portable computer designed for various uses, including work and entertainment."
            },
            new Device
            {
                Id = 3,
                Type = "Tablet",
                Description = "A handheld computer with a touchscreen interface, typically larger than a smartphone."
            },
            new Device
            {
                Id = 4,
                Type = "Desktop Computer",
                Description = "A personal computer designed for use at a single location, such as a desk or table."
            },
            new Device
            {
                Id = 5,
                Type = "Smartwatch",
                Description = "A wearable device that offers various functionalities, including fitness tracking and notifications."
            },
            new Device
            {
                Id = 6,
                Type = "Camera",
                Description = "A device used for capturing photographs or videos."
            },
            new Device
            {
                Id = 7,
                Type = "Headphones",
                Description = "An audio accessory worn over the ears for listening to music or other audio content."
            },
            new Device
            {
                Id = 8,
                Type = "Gaming Console",
                Description = "A specialized device for playing video games on a TV or monitor."
            },
            new Device
            {
                Id = 9,
                Type = "Router",
                Description = "A networking device used to connect multiple devices to the internet or a local network."
            },
            new Device
            {
                Id = 10,
                Type = "Printer",
                Description = "A device for producing physical copies of digital documents or images."
            },
            new Device
            {
                Id = 11,
                Type = "Fitness Tracker",
                Description = "A wearable device designed to monitor fitness-related metrics, such as steps and heart rate."
            },
            new Device
            {
                Id = 12,
                Type = "VR Headset",
                Description = "A device that provides a virtual reality experience, often used for gaming and simulations."
            });
        }
    }
}
