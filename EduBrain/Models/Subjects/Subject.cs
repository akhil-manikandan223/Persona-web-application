using EduBrain.Models.Teachers;
using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.Subjects
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

}
