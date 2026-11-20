using APILab13.DTO.Request;
using APILab13.DTO.Response;
using APILab13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILab13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponseV1>>> GetCourses()
        {
            //Solo los cursos activos (eliminación lógica)
            return await _context.Courses
                .Where(x => x.isActive)
                .Select(x => new CourseResponseV1
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    Credit = x.Credit
                }).ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponseV1>> GetCourse(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(x => x.CourseId == id && x.isActive);

            if (course == null)
            {
                return NotFound();
            }

            return new CourseResponseV1
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Credit = course.Credit
            };
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseResponseV1>> PostCourse(CourseRequestV1 request)
        {
            //Convertir el request a model
            Course model = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                isActive = true
            };

            _context.Courses.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = model.CourseId },
                new CourseResponseV1
                {
                    CourseId = model.CourseId,
                    Name = model.Name,
                    Credit = model.Credit
                });
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseRequestV1 request)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(x => x.CourseId == id && x.isActive);

            if (course == null)
            {
                return NotFound();
            }

            course.Name = request.Name;
            course.Credit = request.Credit;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(x => x.CourseId == id && x.isActive);

            if (course == null)
            {
                return NotFound();
            }

            //Eliminación lógica: no se borra el registro, solo se desactiva
            course.isActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
