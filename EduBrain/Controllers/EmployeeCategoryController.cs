using EduBrain.Data;
using EduBrain.Models.EmployeeCategories;
using Microsoft.AspNetCore.Mvc;

namespace EduBrain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeCategoryController : ControllerBase
    {
        private readonly EduBrainContext _context;

        public EmployeeCategoryController(EduBrainContext context)
        {
            _context = context;
        }

        // GET: api/employeecategory/getallemployeecategories
        [HttpGet("getallemployeecategories")]
        public IActionResult ShowAllEmployeeCategories()
        {
            var employeeCategories = _context.EmployeeCategories.ToList();
            return Ok(employeeCategories);
        }

        // GET: api/employeecategory/getemployeecategorybyid/{id}
        [HttpGet("getemployeecategorybyid/{id}")]
        public IActionResult GetEmployeeCategoryById(int id)
        {
            try
            {
                var employeeCategory = _context.EmployeeCategories.FirstOrDefault(ec => ec.CategoryId == id);

                if (employeeCategory == null)
                {
                    return NotFound($"Employee Category with ID {id} is not found.");
                }
                return Ok(employeeCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/employeecategory/addemployeecategories
        [HttpPost("addemployeecategories")]
        public IActionResult AddEmployeeCategory([FromBody] EmployeeCategory employeeCategoryDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmployeeCategories.Add(employeeCategoryDetails);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployeeCategoryById), new { id = employeeCategoryDetails.CategoryId }, employeeCategoryDetails);
        }

        // PUT: api/employeecategory/updateemployeecategory/{id}
        [HttpPut("updateemployeecategory/{id}")]
        public IActionResult UpdateEmployeeCategory(int id, [FromBody] EmployeeCategory employeeCategoryDetails)
        {
            var employeeCategoryToUpdate = _context.EmployeeCategories.FirstOrDefault(ec => ec.CategoryId == id);

            if (employeeCategoryToUpdate == null)
            {
                return NotFound($"Employee Category with ID {id} is not found.");
            }
            employeeCategoryToUpdate.CategoryName = employeeCategoryDetails.CategoryName;
            _context.EmployeeCategories.Update(employeeCategoryToUpdate);
            _context.SaveChanges();
            return Ok(employeeCategoryToUpdate);
        }

        // DELETE: api/employeecategory/deleteemployeecategory/{id}
        [HttpDelete("deleteemployeecategory/{id}")]
        public IActionResult RemoveEmployeeCategory(int id)
        {
            var employeeCategoryToDelete = _context.EmployeeCategories.FirstOrDefault(ec => ec.CategoryId == id);

            if (employeeCategoryToDelete == null)
            {
                return NotFound($"Employee Category with ID {id} is not found.");
            }

            _context.EmployeeCategories.Remove(employeeCategoryToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

