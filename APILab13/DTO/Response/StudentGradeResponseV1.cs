namespace APILab13.DTO.Response
{
    public class StudentGradeResponseV1
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //Datos del grado al que pertenece
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string GradeDescription { get; set; }
    }
}
