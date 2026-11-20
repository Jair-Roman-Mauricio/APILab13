namespace APILab13.DTO.Response
{
    public class EnrollmentResponseV1
    {
        //Datos de la matrícula
        public int EnrollmentId { get; set; }
        public DateTime Date { get; set; }

        //Datos del estudiante
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //Datos del curso
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int Credit { get; set; }
    }
}
