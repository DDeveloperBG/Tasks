using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options) :
            base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<Homework> HomeworkSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Integrated Security=true;Database=StudentSystem");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Student>()
                .Property(x => x.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength(true)
                .IsUnicode(false);

            modelBuilder
                .Entity<Resource>()
                .Property(x => x.Url)
                .IsUnicode(false);

            modelBuilder
               .Entity<Homework>()
               .Property(x => x.Content)
               .IsUnicode(false);

            modelBuilder
                .Entity<StudentCourse>()
                .HasKey("StudentId", "CourseId");
        }
    }
}
