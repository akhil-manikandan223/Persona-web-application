using EduBrain.Data;
using EduBrain.Models.States;
using Microsoft.AspNetCore.Mvc;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public StateController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/state/getallstates
        [HttpGet("getallstates")]
        public IActionResult ShowAllStates()
        {
            var states = _context.States.ToList();
            return Ok(states);
        }

        // GET: api/state/getstatebyid/{id}
        [HttpGet("getstatebyid/{id}")]
        public IActionResult GetStateById(int id)
        {
            try
            {
                var state = _context.States.FirstOrDefault(c => c.StateId == id);

                if (state == null)
                {
                    return NotFound($"State with ID {id} is not found.");
                }
                return Ok(state);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/state/addstates
        [HttpPost("addstates")]
        public IActionResult AddState([FromBody] State stateDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.States.Add(stateDetails);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStateById), new { id = stateDetails.StateId }, stateDetails);
        }

        // PUT: api/state/updatestate/{id}
        [HttpPut("updatestate/{id}")]
        public IActionResult UpdateState(int id, [FromBody] State stateDetails)
        {
            var stateToUpdate = _context.States.FirstOrDefault(c => c.StateId == id);

            if (stateToUpdate == null)
            {
                return NotFound($"State with ID {id} is not found.");
            }
            stateToUpdate.StateName = stateDetails.StateName;
            _context.States.Update(stateToUpdate);
            _context.SaveChanges();
            return Ok(stateToUpdate);
        }

        // DELETE: api/state/deletestate/{id}
        [HttpDelete("deletestate/{id}")]
        public IActionResult RemoveState(int id)
        {
            var stateToDelete = _context.States.FirstOrDefault(c => c.StateId == id);

            if (stateToDelete == null)
            {
                return NotFound($"State with ID {id} is not found.");
            }

            _context.States.Remove(stateToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

