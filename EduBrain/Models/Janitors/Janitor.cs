using EduBrain.Models.Blocks;
using EduBrain.Models.EmployeeCategories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.Janitors
{
    public class Janitor
    {
        [Key]
        public int EmployeeId { get; set; }  // Primary key for the Janitor entity

        public string JanitorName { get; set; }  // Name of the Janitor
        public string Mobile { get; set; }  // Mobile number of the Janitor
        public string Address { get; set; }  // Address of the Janitor
        public DateTime DateOfBirth { get; set; }  // Date of Birth of the Janitor
        public string Gender { get; set; }  // Gender of the Janitor

        // Foreign key for EmployeeCategory
        [ForeignKey("CategoryName")]
        public int CategoryId { get; set; }
        public virtual EmployeeCategory CategoryName { get; set; }  // Navigation property to EmployeeCategory

        // Foreign key for Block
        [ForeignKey("BlockName")]
        public int BlockId { get; set; }
        public virtual Block BlockName { get; set; }  // Navigation property to Block
    }
}
