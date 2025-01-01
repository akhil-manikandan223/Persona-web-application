using EduBrain.Models.Clubs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    public string RollNumber { get; set; }

    [Required]
    [StringLength(100)]
    public string StudentName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Mobile { get; set; }

    [Required]
    [StringLength(10)]
    public string Gender { get; set; }

    [StringLength(500)]
    public string Address { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    public string Guardian { get; set; }

    public string ClassName { get; set; }

    [ForeignKey("Club")]
    public int ClubId { get; set; }

    [JsonIgnore]
    public virtual Club Club { get; set; }
}
