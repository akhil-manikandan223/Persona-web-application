using EduBrain.Data;
using EduBrain.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public DepartmentController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/department/getalldepartments
        [HttpGet("getalldepartments")]
        public IActionResult ShowAllDepartments()
        {
            var departments = _context.Departments.ToList();
            return Ok(departments);
        }

        // GET: api/department/getdepartmentbyid/{id}
        [HttpGet("getdepartmentbyid/{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);

                if (department == null)
                {
                    return NotFound($"Department with ID {id} is not found.");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/department/adddepartments
        [HttpPost("adddepartments")]
        public IActionResult AddDepartment([FromBody] Department departmentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Departments.Add(departmentDetails);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDepartmentById), new { id = departmentDetails.DepartmentId }, departmentDetails);
        }

        // PUT: api/department/updatedepartment/{id}
        [HttpPut("updatedepartment/{id}")]
        public IActionResult UpdateDepartment(int id, [FromBody] Department departmentDetails)
        {
            var departmentToUpdate = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);

            if (departmentToUpdate == null)
            {
                return NotFound($"Department with ID {id} is not found.");
            }
            departmentToUpdate.DepartmentName = departmentDetails.DepartmentName;
            _context.Departments.Update(departmentToUpdate);
            _context.SaveChanges();
            return Ok(departmentToUpdate);
        }

        // DELETE: api/department/deletedepartment/{id}
        [HttpDelete("deletedepartment/{id}")]
        public IActionResult RemoveDepartment(int id)
        {
            var departmentToDelete = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);

            if (departmentToDelete == null)
            {
                return NotFound($"Department with ID {id} is not found.");
            }

            _context.Departments.Remove(departmentToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
