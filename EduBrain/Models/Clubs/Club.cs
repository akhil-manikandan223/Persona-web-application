using System.ComponentModel.DataAnnotations;

namespace EduBrain.Models.Clubs
{
    public class Club
    {
        [Key]
        public int ClubId { get; set; }

        [Required]
        public string ClubName { get; set; }

        // Navigation property
        public virtual ICollection<Student> Students { get; set; }
    }

}
