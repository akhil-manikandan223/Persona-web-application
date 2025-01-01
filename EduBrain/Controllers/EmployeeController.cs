using EduBrain.Data;
using EduBrain.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly EduBrainContext _context;
        public EmployeeController(EduBrainContext context)
        {
            _context = context;
        }
        // GET: api/employee/getallemployees
        [HttpGet("getallemployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Include(t => t.Category)
                .Include(t => t.Department)
                .Include(t => t.Subject)
                .Include(t => t.Club)
                .Include(t => t.Location)
                .Include(t => t.State)
                .ToListAsync();

            return Ok(employees);
        }

        // GET: api/employee/getemployeebyid/{id}
        [HttpGet("getemployeebyid/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .Include(t => t.Category)
                .Include(t => t.Department)
                .Include(t => t.Subject)
                .Include(t => t.Club)
                .Include(t => t.Location) // Add this
                .Include(t => t.State)
                .FirstOrDefaultAsync(t => t.EmployeeId == id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} is not found.");
            }
            return Ok(employee);
        }

        // POST: api/employee/addemployee
        [HttpPost("addemployee")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                EmployeeName = employeeDto.EmployeeName,
                Email = employeeDto.Email,
                Mobile = employeeDto.Mobile,
                Gender = employeeDto.Gender,
                AddressLine1 = employeeDto.AddressLine1,
                AddressLine2 = employeeDto.AddressLine2,
                LocationId = employeeDto.LocationId,
                StateId = employeeDto.StateId,
                DateOfBirth = employeeDto.DateOfBirth,
                CategoryId = employeeDto.CategoryId,
                DepartmentId = employeeDto.DepartmentId,
                SubjectId = employeeDto.SubjectId,
                ClubId = employeeDto.ClubId
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
        }

        [HttpPut("updateemployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId)
            {
                return BadRequest("ID mismatch.");
            }

            var employeeToUpdate = await _context.Employees
                .Include(t => t.Location)
                .Include(t => t.State)
                .FirstOrDefaultAsync(t => t.EmployeeId == id);

            if (employeeToUpdate == null)
            {
                return NotFound($"Employee with ID {id} is not found.");
            }

            var location = await _context.Locations.FindAsync(employeeDto.LocationId);
            if (location == null)
            {
                return BadRequest($"Location with ID {employeeDto.LocationId} is not found.");
            }

            var state = await _context.States.FindAsync(employeeDto.StateId);
            if (state == null)
            {
                return BadRequest($"State with ID {employeeDto.StateId} is not found.");
            }

            employeeToUpdate.EmployeeName = employeeDto.EmployeeName;
            employeeToUpdate.Email = employeeDto.Email;
            employeeToUpdate.Mobile = employeeDto.Mobile;
            employeeToUpdate.Gender = employeeDto.Gender;
            employeeToUpdate.AddressLine1 = employeeDto.AddressLine1;
            employeeToUpdate.AddressLine2 = employeeDto.AddressLine2;
            employeeToUpdate.Location = location;
            employeeToUpdate.State = state;
            employeeToUpdate.DateOfBirth = employeeDto.DateOfBirth;
            employeeToUpdate.CategoryId = employeeDto.CategoryId;
            employeeToUpdate.DepartmentId = employeeDto.DepartmentId;
            employeeToUpdate.SubjectId = employeeDto.SubjectId;
            employeeToUpdate.ClubId = employeeDto.ClubId;

            _context.Employees.Update(employeeToUpdate);
            await _context.SaveChangesAsync();
            return Ok(employeeToUpdate);
        }

        // DELETE: api/employee/deleteemployee/{id}
        [HttpDelete("deleteemployee/{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {
            var employeeToDelete = await _context.Employees.FirstOrDefaultAsync(t => t.EmployeeId == id);
            if (employeeToDelete == null)
            {
                return NotFound($"Employee with ID {id} is not found.");
            }

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // DTO Class for Employee
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int LocationId { get; set; }
        public int StateId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public int SubjectId { get; set; }
        public int ClubId { get; set; }
    }
}
