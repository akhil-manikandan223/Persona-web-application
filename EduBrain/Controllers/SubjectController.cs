using EduBrain.Data;
using EduBrain.Models.Subjects;
using Microsoft.AspNetCore.Mvc;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public SubjectController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/subject/getallsubjects
        [HttpGet("getallsubjects")]
        public IActionResult ShowAllSubjects()
        {
            var subjects = _context.Subjects.ToList();
            return Ok(subjects);
        }

        // GET: api/subject/getsubjectbyid/{id}
        [HttpGet("getsubjectbyid/{id}")]
        public IActionResult GetSubjectById(int id)
        {
            try
            {
                var subject = _context.Subjects.FirstOrDefault(s => s.SubjectId == id);

                if (subject == null)
                {
                    return NotFound($"Subject with ID {id} is not found.");
                }
                return Ok(subject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/subject/addsubjects
        [HttpPost("addsubjects")]
        public IActionResult AddSubject([FromBody] Subject subjectDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subjects.Add(subjectDetails);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSubjectById), new { id = subjectDetails.SubjectId }, subjectDetails);
        }

        // PUT: api/subject/updatesubject/{id}
        [HttpPut("updatesubject/{id}")]
        public IActionResult UpdateSubject(int id, [FromBody] Subject subjectDetails)
        {
            var subjectToUpdate = _context.Subjects.FirstOrDefault(s => s.SubjectId == id);

            if (subjectToUpdate == null)
            {
                return NotFound($"Subject with ID {id} is not found.");
            }
            subjectToUpdate.SubjectName = subjectDetails.SubjectName;
            _context.Subjects.Update(subjectToUpdate);
            _context.SaveChanges();
            return Ok(subjectToUpdate);
        }

        // DELETE: api/subject/deletesubject/{id}
        [HttpDelete("deletesubject/{id}")]
        public IActionResult RemoveSubject(int id)
        {
            var subjectToDelete = _context.Subjects.FirstOrDefault(s => s.SubjectId == id);

            if (subjectToDelete == null)
            {
                return NotFound($"Subject with ID {id} is not found.");
            }

            _context.Subjects.Remove(subjectToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

