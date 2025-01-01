using EduBrain.Models.Teachers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.Classes
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        public string ClassName { get; set; }

        [ForeignKey("TeacherName")]
        public int EmployeeId { get; set; }
        public virtual Teacher TeacherName { get; set; }
    }
}
