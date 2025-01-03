﻿using EduBrain.Models.EmployeeCategories;
using EduBrain.Models.Vehicles;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.Drivers
{
    public class Driver
    {
        [Key]
        public int EmployeeId { get; set; } // Primary key

        public string DriverName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        // Foreign key for Employee Category
        [ForeignKey("CategoryName")]
        public int CategoryId { get; set; }
        public virtual EmployeeCategory CategoryName { get; set; } // Navigation property for EmployeeCategory

        // Foreign key for Vehicle
        [ForeignKey("VehicleNumber")]
        public int VehicleId { get; set; }
        public virtual Vehicle VehicleNumber { get; set; } // Navigation property for Vehicle
    }
}
