using EduBrain.Models.Blocks;
using EduBrain.Models.EmployeeCategories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.Watchmen
{
    public class Watchman
    {
        [Key]
        public int EmployeeId { get; set; }  // Primary key for the Watchman entity

        public string WatchmanName { get; set; }  // Name of the watchman
        public string Mobile { get; set; }  // Mobile number of the watchman
        public string Address { get; set; }  // Address of the watchman
        public DateTime DateOfBirth { get; set; }  // Date of birth of the watchman
        public string Gender { get; set; }  // Gender of the watchman

        // Foreign key for EmployeeCategory
        [ForeignKey("CategoryName")]  // Specify the navigation property for the foreign key
        public int CategoryId { get; set; }
        public virtual EmployeeCategory CategoryName { get; set; }  // Navigation property to EmployeeCategory

        // Foreign key for Block
        [ForeignKey("BlockName")]  // Specify the navigation property for the foreign key
        public int BlockId { get; set; }
        public virtual Block BlockName { get; set; }  // Navigation property to Block
    }
}
