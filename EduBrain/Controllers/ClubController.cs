using EduBrain.Data;
using EduBrain.Models.Clubs;
using Microsoft.AspNetCore.Mvc;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public ClubController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/club/getallclubs
        [HttpGet("getallclubs")]
        public IActionResult ShowAllClubs()
        {
            var clubs = _context.Clubs.ToList();
            return Ok(clubs);
        }

        // GET: api/club/getclubbyid/{id}
        [HttpGet("getclubbyid/{id}")]
        public IActionResult GetClubById(int id)
        {
            try
            {
                var club = _context.Clubs.FirstOrDefault(c => c.ClubId == id);

                if (club == null)
                {
                    return NotFound($"Club with ID {id} is not found.");
                }
                return Ok(club);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/club/addclubs
        [HttpPost("addclubs")]
        public IActionResult AddClub([FromBody] Club clubDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clubs.Add(clubDetails);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClubById), new { id = clubDetails.ClubId }, clubDetails);
        }

        // PUT: api/club/updateclub/{id}
        [HttpPut("updateclub/{id}")]
        public IActionResult UpdateClub(int id, [FromBody] Club clubDetails)
        {
            var clubToUpdate = _context.Clubs.FirstOrDefault(c => c.ClubId == id);

            if (clubToUpdate == null)
            {
                return NotFound($"Club with ID {id} is not found.");
            }
            clubToUpdate.ClubName = clubDetails.ClubName;
            _context.Clubs.Update(clubToUpdate);
            _context.SaveChanges();
            return Ok(clubToUpdate);
        }

        // DELETE: api/club/deleteclub/{id}
        [HttpDelete("deleteclub/{id}")]
        public IActionResult RemoveClub(int id)
        {
            var clubToDelete = _context.Clubs.FirstOrDefault(c => c.ClubId == id);

            if (clubToDelete == null)
            {
                return NotFound($"Club with ID {id} is not found.");
            }

            _context.Clubs.Remove(clubToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

