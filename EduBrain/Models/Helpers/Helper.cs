using EduBrain.Models.EmployeeCategories;
using EduBrain.Models.Vehicles;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.Helpers
{
    public class Helper
    {
        [Key]
        public int EmployeeId { get; set; } // Primary key

        [Required] // Indicates that this field is mandatory
        public string HelperName { get; set; }

        [Phone] // Optional: ensures it's a valid phone number format
        public string Mobile { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)] // Optional: specifies that this is a date
        public DateTime DateOfBirth { get; set; }

        [Required] // Indicates that this field is mandatory
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
