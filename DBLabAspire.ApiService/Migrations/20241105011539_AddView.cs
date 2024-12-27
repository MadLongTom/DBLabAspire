using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLabAspire.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW View_HighestAverageGradeCourse AS
                SELECT c.""Cno"", c.""Cname"", AVG(cl.""Grade"") AS AverageGrade
                FROM ""Course"" c
                JOIN ""Claims"" cl ON c.""Cno"" = cl.""Cno""
                GROUP BY c.""Cno"", c.""Cname""
                ORDER BY AverageGrade DESC
                LIMIT 1;
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW View_HighestAverageGradeCourseClaims AS
                SELECT s.""Sno"", s.""Sname"", c.""Cname"", cl.""Grade""
                FROM ""Student"" s
                JOIN ""Claims"" cl ON s.""Sno"" = cl.""Sno""
                JOIN ""Course"" c ON cl.""Cno"" = c.""Cno""
                WHERE c.""Cno"" = (SELECT ""Cno"" FROM View_HighestAverageGradeCourse);
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW View_HighestAverageGradeStudentDepartment AS
                SELECT s.""Smajor"", AVG(cl.""Grade"") AS AverageGrade
                FROM ""Student"" s
                JOIN ""Claims"" cl ON s.""Sno"" = cl.""Sno""
                GROUP BY s.""Smajor""
                ORDER BY AverageGrade DESC
                LIMIT 1;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS View_HighestAverageGradeCourse;");
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS View_HighestAverageGradeCourseClaims;");
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS View_HighestAverageGradeStudentDepartment;");
        }
    }
}

