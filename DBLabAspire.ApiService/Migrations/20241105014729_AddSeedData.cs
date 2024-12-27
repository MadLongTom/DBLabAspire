using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DBLabAspire.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Cno", "Ccredit", "Cname", "Cpno" },
                values: new object[,]
                {
                    { "C001", (short)3, "Introduction to Computer Science", null },
                    { "C002", (short)4, "Calculus I", null },
                    { "C003", (short)4, "Physics I", null }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Sno", "Sbirthdate", "Smajor", "Sname", "Ssex" },
                values: new object[,]
                {
                    { "S000001", new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "CS", "Alice", "FM" },
                    { "S000002", new DateTimeOffset(new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "CS", "Bob", "M" },
                    { "S000003", new DateTimeOffset(new DateTime(2000, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Physics", "Charlie", "Male" }
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Cno", "Sno", "Grade", "Semester", "Teachingclass" },
                values: new object[,]
                {
                    { "C001", "S000001", (short)85, "2021F", "TC001" },
                    { "C002", "S000001", (short)90, "2021F", "TC002" },
                    { "C002", "S000002", (short)75, "2021F", "TC002" },
                    { "C003", "S000003", (short)80, "2021F", "TC003" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumns: new[] { "Cno", "Sno" },
                keyValues: new object[] { "C001", "S000001" });

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumns: new[] { "Cno", "Sno" },
                keyValues: new object[] { "C002", "S000001" });

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumns: new[] { "Cno", "Sno" },
                keyValues: new object[] { "C002", "S000002" });

            migrationBuilder.DeleteData(
                table: "Claims",
                keyColumns: new[] { "Cno", "Sno" },
                keyValues: new object[] { "C003", "S000003" });

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Cno",
                keyValue: "C001");

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Cno",
                keyValue: "C002");

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "Cno",
                keyValue: "C003");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Sno",
                keyValue: "S000001");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Sno",
                keyValue: "S000002");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "Sno",
                keyValue: "S000003");
        }
    }
}
