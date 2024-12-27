using DBLabAspire.ApiService.Models;
using DBLabAspire.ApiService.Models.View;
using Microsoft.EntityFrameworkCore;

namespace DBLabAspire.ApiService.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<HighestAverageGradeCourse> HighestAverageGradeCourses { get; set; }
        public DbSet<HighestAverageGradeCourseClaims> HighestAverageGradeCourseClaims { get; set; }
        public DbSet<HighestAverageGradeStudentDepartment> HighestAverageGradeStudentDepartments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Name)
                .IsUnique();
            modelBuilder.Entity<Course>()
               .HasOne(c => c.PrerequisiteCourse)
               .WithMany()
               .HasForeignKey(c => c.PrerequisiteCourseId);
            modelBuilder.Entity<Claims>()
                .HasKey(c => new { c.StudentId, c.CourseId });
            modelBuilder.Entity<Claims>()
                .HasOne(c => c.Student)
                .WithMany()
                .HasForeignKey(c => c.StudentId);
            modelBuilder.Entity<Claims>()
                .HasOne(c => c.Course)
                .WithMany()
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<HighestAverageGradeCourse>()
                .ToView("view_highestaveragegradecourse")
                .HasKey(v => v.Cno);
            modelBuilder.Entity<HighestAverageGradeCourseClaims>()
                .ToView("view_highestaveragegradecourseclaims")
                .HasKey(v => new { v.Sno, v.Cname });
            modelBuilder.Entity<HighestAverageGradeStudentDepartment>()
                .ToView("view_highestaveragegradestudentdepartment")
                .HasKey(v => v.Smajor);

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = "S000001", Name = "Alice", Sex = "FM", BirthDate = new DateTimeOffset(new DateTime(2000, 1, 1)), Major = "CS" },
                new Student { Id = "S000002", Name = "Bob", Sex = "M", BirthDate = new DateTimeOffset(new DateTime(2000, 2, 2)), Major = "CS" },
                new Student { Id = "S000003", Name = "Charlie", Sex = "Male", BirthDate = new DateTimeOffset(new DateTime(2000, 3, 3)), Major = "Physics" }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = "C001", Name = "Introduction to Computer Science", Credit = 3 },
                new Course { Id = "C002", Name = "Calculus I", Credit = 4 },
                new Course { Id = "C003", Name = "Physics I", Credit = 4 }
            );

            // Seed Claims
            modelBuilder.Entity<Claims>().HasData(
                new Claims { StudentId = "S000001", CourseId = "C001", Grade = 85, Semester = "2021F", TeachingClass = "TC001" },
                new Claims { StudentId = "S000001", CourseId = "C002", Grade = 90, Semester = "2021F", TeachingClass = "TC002" },
                new Claims { StudentId = "S000002", CourseId = "C002", Grade = 75, Semester = "2021F", TeachingClass = "TC002" },
                new Claims { StudentId = "S000003", CourseId = "C003", Grade = 80, Semester = "2021F", TeachingClass = "TC003" }
            );

        }
    }
}
