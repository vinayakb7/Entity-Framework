using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiWithMSSQL.Models;

namespace WebApiWithMSSQL.Controllers
{
    [Route("StudentPortalByVinayak")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public StudentController(DBContext brandContext)
        {
            _dbContext = brandContext;
        }

        [HttpGet("StudentLists")]
        public async Task<ActionResult<IEnumerable<Student>>> GetBrands()
        {
            if (_dbContext.Student is null) return NotFound();
            return await _dbContext.Student.ToListAsync();
        }

        [HttpGet("Student/{id}")]
        public async Task<ActionResult<Student>> GetBrands(int id)
        {
            if (_dbContext.Student is null) return NotFound();
            var brand = await _dbContext.Student.FindAsync(id);
            if (brand is null) return NotFound();
            return brand;
        }

        [HttpPost("Student/Insert")]
        public async Task<ActionResult<IEnumerable<Student>>> PostBrands(Student brand)
        {
            _dbContext.Student.Add(brand);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrands), new { id = brand.Id }, brand);
        }

        [HttpPut("Student/Update")]
        public async Task<IActionResult> PutBrand(Student brand)
        {
            _dbContext.Entry(brand).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!_dbContext.Student.Any(x => x.Id == brand.Id)) return NotFound();
                else throw;
            }
            return Ok();
        }

        [HttpDelete("Student/Delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Student is null) return NotFound();
            var brand = await _dbContext.Student.FindAsync(id);
            if (brand is null) return NotFound();
            _dbContext.Student.Remove(brand);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Student/Query")]
        public List<Student> GetBrandsWithCustomQuery()
        {
            return _dbContext.Student.FromSql($"select * from Brand").Select(x=> new Student() { Name = x.Name, Address = x.Address }).ToList();
        }

        [HttpGet("Student/Join")]
        public IQueryable<dynamic> GetBrandsWithJoin()
        {
            return from brand in _dbContext.Student
                          join brandTest in _dbContext.StudentDetails on brand.Id equals brandTest.StudentId
                          select new
                          {
                              BrandId = brand.Id,
                              BrandDiscription = brandTest.SchoolName
                          };
        }
    }
}
