using APILab13.DTO.Response;
using APILab13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILab13.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsCustomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsCustomController(ApplicationDbContext context)
        {
            _context = context;
        }

        //8. Estudiantes por nombre, apellido y email ordenados desc. por apellido
        [HttpGet]
        public List<StudentResponseV1> GetByNameLastNameEmail(
            string? firstName, string? lastName, string? email)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName));
            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(x => x.LastName.Contains(lastName));
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.Email.Contains(email));

            return query
                .OrderByDescending(x => x.LastName)
                .Select(x => new StudentResponseV1
                {
                    StudentId = x.StudentId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    Email = x.Email
                }).ToList();
        }

        //9. Estudiantes por nombre y grado con los datos del grado,
        //   ordenados de forma descendente por el nombre del grado
        [HttpGet]
        public List<StudentGradeResponseV1> GetByNameAndGrade(
            string? firstName, string? gradeName)
        {
            var query = _context.Students
                .Include(x => x.Grade)
                .Where(x => x.Grade.isActive);

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName));
            if (!string.IsNullOrWhiteSpace(gradeName))
                query = query.Where(x => x.Grade.Name.Contains(gradeName));

            return query
                .OrderByDescending(x => x.Grade.Name)
                .Select(x => new StudentGradeResponseV1
                {
                    StudentId = x.StudentId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    Email = x.Email,
                    GradeId = x.GradeId,
                    GradeName = x.Grade.Name,
                    GradeDescription = x.Grade.Description
                }).ToList();
        }

        //10. Estudiantes matriculados buscando por nombre del curso,
        //    ordenados por curso y luego por apellido
        [HttpGet]
        public List<EnrollmentResponseV1> GetEnrolledByCourse(string courseName)
        {
            return _context.Enrollments
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Where(x => x.Course.isActive && x.Course.Name.Contains(courseName))
                .OrderBy(x => x.Course.Name)
                .ThenBy(x => x.Student.LastName)
                .Select(x => new EnrollmentResponseV1
                {
                    EnrollmentId = x.EnrollmentId,
                    Date = x.Date,
                    StudentId = x.StudentId,
                    FirstName = x.Student.FirstName,
                    LastName = x.Student.LastName,
                    Email = x.Student.Email,
                    CourseId = x.CourseId,
                    CourseName = x.Course.Name,
                    Credit = x.Course.Credit
                }).ToList();
        }

        //11. Estudiantes matriculados buscando por grado,
        //    ordenados por curso y luego por apellido
        [HttpGet]
        public List<EnrollmentResponseV1> GetEnrolledByGrade(string gradeName)
        {
            return _context.Enrollments
                .Include(x => x.Student).ThenInclude(s => s.Grade)
                .Include(x => x.Course)
                .Where(x => x.Course.isActive
                         && x.Student.Grade.isActive
                         && x.Student.Grade.Name.Contains(gradeName))
                .OrderBy(x => x.Course.Name)
                .ThenBy(x => x.Student.LastName)
                .Select(x => new EnrollmentResponseV1
                {
                    EnrollmentId = x.EnrollmentId,
                    Date = x.Date,
                    StudentId = x.StudentId,
                    FirstName = x.Student.FirstName,
                    LastName = x.Student.LastName,
                    Email = x.Student.Email,
                    CourseId = x.CourseId,
                    CourseName = x.Course.Name,
                    Credit = x.Course.Credit
                }).ToList();
        }
    }
}
