using EduBrain.Models.Clubs;
using EduBrain.Models.Teachers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduBrain.Models.ClubRepresentatives
{
    public class ClubRep
    {
        [Key]
        public int Id { get; set; }  // Primary key

        [ForeignKey("ClubName")]
        public int ClubId { get; set; }
        public virtual Club ClubName { get; set; }  // Navigation to Club entity

        [ForeignKey("StudentName")]
        public int StudentId { get; set; }
        public virtual Student StudentName { get; set; }  // Navigation to Student entity

        [ForeignKey("TeacherName")]
        public int EmployeeId { get; set; }
        public virtual Teacher TeacherName { get; set; }  // Navigation to Student entity
    }
}

