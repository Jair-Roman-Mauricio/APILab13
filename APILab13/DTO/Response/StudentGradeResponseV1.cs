namespace APILab13.DTO.Response
{
    public class StudentGradeResponseV1
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //Datos del grado al que pertenece
        public int GradeId { get; set; }
        public string GradeName { get; set; } = string.Empty;
        public string GradeDescription { get; set; } = string.Empty;
    }
}
