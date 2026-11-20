using Microsoft.EntityFrameworkCore;

namespace APILab13.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //VARCHAR(45) según el diagrama
            modelBuilder.Entity<Grade>(e =>
            {
                e.Property(x => x.Name).HasColumnType("varchar(45)");
                e.Property(x => x.Description).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.Property(x => x.Name).HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.Property(x => x.FirstName).HasColumnType("varchar(45)");
                e.Property(x => x.LastName).HasColumnType("varchar(45)");
                e.Property(x => x.Phone).HasColumnType("varchar(45)");
                e.Property(x => x.Email).HasColumnType("varchar(45)");

                //Student N - 1 Grade
                e.HasOne(x => x.Grade)
                 .WithMany(g => g.Students)
                 .HasForeignKey(x => x.GradeId);
            });

            modelBuilder.Entity<Enrollment>(e =>
            {
                //Enrollment N - 1 Student
                e.HasOne(x => x.Student)
                 .WithMany(s => s.Enrollments)
                 .HasForeignKey(x => x.StudentId);

                //Enrollment N - 1 Course
                e.HasOne(x => x.Course)
                 .WithMany(c => c.Enrollments)
                 .HasForeignKey(x => x.CourseId);
            });
        }
    }
}
