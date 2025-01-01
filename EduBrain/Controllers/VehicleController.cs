using EduBrain.Data;
using EduBrain.Models.EmployeeCategories;
using EduBrain.Models.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController: ControllerBase
    {
        private readonly EduBrainContext _context;

        public VehicleController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/vehicle/getallvehicles
        [HttpGet("getallvehicles")]
        public IActionResult ShowAllVehicles()
        {
            var vehicles = _context.Vehicles.ToList();
            return Ok(vehicles);
        }

        // GET: api/vehicle/getvehiclebyid/{id}
        [HttpGet("getvehiclebyid/{id}")]
        public IActionResult GetVehicleById(int id)
        {
            try
            {
                var vehicle = _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);

                if (vehicle == null)
                {
                    return NotFound($"Vehicle with ID {id} is not found.");
                }
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/vehicle/addvehicles
        [HttpPost("addvehicles")]
        public IActionResult AddVehicle([FromBody] Vehicle vehicleDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vehicles.Add(vehicleDetails);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicleDetails.VehicleId }, vehicleDetails);
        }

        // PUT: api/vehicle/updatevehicle/{id}
        [HttpPut("updatevehicle/{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] Vehicle vehicleDetails)
        {
            var vehicleToUpdate = _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicleToUpdate == null)
            {
                return NotFound($"Vehicle with ID {id} is not found.");
            }
            vehicleToUpdate.VehicleNumber = vehicleDetails.VehicleNumber;
            _context.Vehicles.Update(vehicleToUpdate);
            _context.SaveChanges();
            return Ok(vehicleToUpdate);
        }

        // DELETE: api/vehicle/deletevehicle/{id}
        [HttpDelete("deletevehicle/{id}")]
        public IActionResult RemoveVehicle(int id)
        {
            var vehicleToDelete = _context.Vehicles.FirstOrDefault(v => v.VehicleId == id);

            if (vehicleToDelete == null)
            {
                return NotFound($"Vehicle with ID {id} is not found.");
            }

            _context.Vehicles.Remove(vehicleToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
