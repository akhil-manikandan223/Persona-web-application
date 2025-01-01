using EduBrain.Models.Departments;
using EduBrain.Models.EmployeeCategories;
using EduBrain.Models.Teachers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.HODs
{
    public class HOD
    {
        [Key]
        public int Id { get; set; }  // Primary key

        // Foreign key to the Teacher entity
        [ForeignKey("TeacherName")]
        public int EmployeeId { get; set; }
        public virtual Teacher TeacherName { get; set; } // Navigation property to Teacher

        // Foreign key to the Department entity
        [ForeignKey("DepartmentName")]
        public int DepartmentId { get; set; }
        public virtual Department DepartmentName { get; set; } // Navigation property to Department
    }
}
