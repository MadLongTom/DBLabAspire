using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLabAspire.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartNo",
                table: "Student",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartNo = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartNo);
                });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Cno",
                keyValue: "C001",
                column: "TeacherName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Cno",
                keyValue: "C002",
                column: "TeacherName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Cno",
                keyValue: "C003",
                column: "TeacherName",
                value: "");

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Sno",
                keyValue: "S000001",
                column: "DepartNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Sno",
                keyValue: "S000002",
                column: "DepartNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "Sno",
                keyValue: "S000003",
                column: "DepartNo",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Student_DepartNo",
                table: "Student",
                column: "DepartNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Departments_DepartNo",
                table: "Student",
                column: "DepartNo",
                principalTable: "Departments",
                principalColumn: "DepartNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Departments_DepartNo",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Student_DepartNo",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DepartNo",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Course");
        }
    }
}
