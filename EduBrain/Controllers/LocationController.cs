using EduBrain.Data;
using EduBrain.Models.Locations;
using EduBrain.Models.States;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public LocationController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/location/getalllocations
        [HttpGet("getalllocations")]
        public async Task<IActionResult> ShowAllLocations()
        {
            try
            {
                // Include State to show the related state information
                var locations = await _context.Locations.Include(l => l.State).ToListAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/location/getlocationbyid/{id}
        [HttpGet("getlocationbyid/{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            try
            {
                // Include State to show related state information
                var location = await _context.Locations.Include(l => l.State)
                    .FirstOrDefaultAsync(c => c.LocationId == id);

                if (location == null)
                {
                    return NotFound($"Location with ID {id} not found.");
                }
                return Ok(location);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/location/addlocations
        [HttpPost("addlocations")]
        public async Task<IActionResult> AddLocation(Location locationDetails)
        {
            // Check if the StateId exists in the database
            var state = await _context.States.FindAsync(locationDetails.StateId);
            if (state == null)
            {
                return NotFound($"State with ID {locationDetails.StateId} not found.");
            }

            // If the state is valid, associate it with the location
            locationDetails.State = state;

            // Add the new location to the database
            _context.Locations.Add(locationDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocationById), new { id = locationDetails.LocationId }, locationDetails);
        }



        // PUT: api/location/updatelocation/{id}
        [HttpPut("updatelocation/{id}")]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] Location locationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationToUpdate = await _context.Locations.FirstOrDefaultAsync(c => c.LocationId == id);

            if (locationToUpdate == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            // Ensure the State exists before updating the location
            var state = await _context.States.FirstOrDefaultAsync(s => s.StateId == locationDetails.StateId);
            if (state == null)
            {
                return BadRequest($"State with ID {locationDetails.StateId} does not exist.");
            }

            // Update location details
            locationToUpdate.LocationName = locationDetails.LocationName;
            locationToUpdate.StateId = locationDetails.StateId;

            _context.Locations.Update(locationToUpdate);
            await _context.SaveChangesAsync();
            return Ok(locationToUpdate);
        }

        // DELETE: api/location/deletelocation/{id}
        [HttpDelete("deletelocation/{id}")]
        public async Task<IActionResult> RemoveLocation(int id)
        {
            var locationToDelete = await _context.Locations.FirstOrDefaultAsync(c => c.LocationId == id);

            if (locationToDelete == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            _context.Locations.Remove(locationToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
