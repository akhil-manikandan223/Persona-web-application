using EduBrain.Models.Teachers;
using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.EmployeeCategories
{
    public class EmployeeCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

}
