using EduBrain.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for Include() method
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public StudentController(EduBrainContext context)
        {
            _context = context;
        }

        // DTO definition (RollNumber removed as it will be auto-assigned)
        public class StudentDto
        {
            public int StudentId { get; set; }

            [Required]
            public string StudentName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            public string Mobile { get; set; }

            public string Gender { get; set; }
            public string Address { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Guardian { get; set; }
            public string ClassName { get; set; }

            [Required]
            public int ClubId { get; set; } // Only keep clubId here
        }

        // GET: api/student/getallstudents
        [HttpGet("getallstudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students
                .Include(s => s.Club) // If you want to include related entities
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/student/getstudentbyid/{id}
        [HttpGet("getstudentbyid/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.Students
                .Include(s => s.Club) // Include related entity if needed
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} is not found.");
            }

            return Ok(student);
        }

        // POST: api/student/addstudent
        [HttpPost("addstudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = new Student
                {
                    StudentName = studentDto.StudentName,
                    Email = studentDto.Email,
                    Mobile = studentDto.Mobile,
                    Gender = studentDto.Gender,
                    Address = studentDto.Address,
                    DateOfBirth = studentDto.DateOfBirth,
                    Guardian = studentDto.Guardian,
                    ClassName = studentDto.ClassName,
                    ClubId = studentDto.ClubId
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                // Recalculate roll numbers after adding the student
                await RecalculateRollNumbers();

                return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/student/updatestudent/{id}
        [HttpPut("updatestudent/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (id != studentDto.StudentId)
            {
                return BadRequest("ID mismatch.");
            }

            var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if (studentToUpdate == null)
            {
                return NotFound($"Student with ID {id} is not found.");
            }

            // Update the fields
            studentToUpdate.StudentName = studentDto.StudentName;
            studentToUpdate.Email = studentDto.Email;
            studentToUpdate.Mobile = studentDto.Mobile;
            studentToUpdate.Gender = studentDto.Gender;
            studentToUpdate.Address = studentDto.Address;
            studentToUpdate.DateOfBirth = studentDto.DateOfBirth;
            studentToUpdate.Guardian = studentDto.Guardian;
            studentToUpdate.ClassName = studentDto.ClassName;
            studentToUpdate.ClubId = studentDto.ClubId;

            _context.Students.Update(studentToUpdate);
            await _context.SaveChangesAsync();

            // Recalculate roll numbers after updating the student
            await RecalculateRollNumbers();

            return Ok(studentToUpdate);
        }

        // DELETE: api/student/deletestudent/{id}
        [HttpDelete("deletestudent/{id}")]
        public async Task<IActionResult> RemoveStudent(int id)
        {
            var studentToDelete = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if (studentToDelete == null)
            {
                return NotFound($"Student with ID {id} is not found.");
            }

            _context.Students.Remove(studentToDelete);
            await _context.SaveChangesAsync();

            // Recalculate roll numbers after deleting a student
            await RecalculateRollNumbers();

            return NoContent();
        }

        // Method to recalculate roll numbers after any add/update/delete
        private async Task RecalculateRollNumbers()
        {
            // Fetch all students and order them by their names alphabetically
            var students = await _context.Students
                .OrderBy(s => s.StudentName)
                .ToListAsync();

            // Assign new roll numbers in alphabetical order
            int rollNumber = 1;
            foreach (var student in students)
            {
                student.RollNumber = rollNumber.ToString("D3"); // Customize the roll number format if needed
                rollNumber++;
            }

            // Save changes to the database
            _context.Students.UpdateRange(students);
            await _context.SaveChangesAsync();
        }
    }
}
