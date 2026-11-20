namespace APILab13.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime Date { get; set; }

        //Llave foránea hacia Student
        public int StudentId { get; set; }
        public Student Student { get; set; }

        //Llave foránea hacia Course
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
