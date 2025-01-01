using EduBrain.Models.Teachers;
using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.Departments
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

}
