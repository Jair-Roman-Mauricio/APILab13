using APILab13.DTO.Request;
using APILab13.DTO.Response;
using APILab13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILab13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeResponseV1>>> GetGrades()
        {
            //Solo los grados activos (eliminación lógica)
            return await _context.Grades
                .Where(x => x.isActive)
                .Select(x => new GradeResponseV1
                {
                    GradeId = x.GradeId,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();
        }

        // GET: api/Grades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeResponseV1>> GetGrade(int id)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(x => x.GradeId == id && x.isActive);

            if (grade == null)
            {
                return NotFound();
            }

            return new GradeResponseV1
            {
                GradeId = grade.GradeId,
                Name = grade.Name,
                Description = grade.Description
            };
        }

        // POST: api/Grades
        [HttpPost]
        public async Task<ActionResult<GradeResponseV1>> PostGrade(GradeRequestV1 request)
        {
            //Convertir el request a model
            Grade model = new Grade
            {
                Name = request.Name,
                Description = request.Description,
                isActive = true
            };

            _context.Grades.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrade), new { id = model.GradeId },
                new GradeResponseV1
                {
                    GradeId = model.GradeId,
                    Name = model.Name,
                    Description = model.Description
                });
        }

        // PUT: api/Grades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, GradeRequestV1 request)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(x => x.GradeId == id && x.isActive);

            if (grade == null)
            {
                return NotFound();
            }

            grade.Name = request.Name;
            grade.Description = request.Description;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Grades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(x => x.GradeId == id && x.isActive);

            if (grade == null)
            {
                return NotFound();
            }

            //Eliminación lógica: no se borra el registro, solo se desactiva
            grade.isActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
