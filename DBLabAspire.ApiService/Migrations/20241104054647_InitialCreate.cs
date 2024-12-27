using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBLabAspire.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Cno = table.Column<string>(type: "CHAR(5)", nullable: false),
                    Cname = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: false),
                    Ccredit = table.Column<short>(type: "smallint", nullable: false),
                    Cpno = table.Column<string>(type: "CHAR(5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Cno);
                    table.ForeignKey(
                        name: "FK_Course_Course_Cpno",
                        column: x => x.Cpno,
                        principalTable: "Course",
                        principalColumn: "Cno");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Sno = table.Column<string>(type: "CHAR(8)", nullable: false),
                    Sname = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Ssex = table.Column<string>(type: "CHAR(6)", maxLength: 6, nullable: false),
                    Sbirthdate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Smajor = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Sno);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Sno = table.Column<string>(type: "CHAR(8)", nullable: false),
                    Cno = table.Column<string>(type: "CHAR(5)", nullable: false),
                    Grade = table.Column<short>(type: "smallint", nullable: true),
                    Semester = table.Column<string>(type: "CHAR(5)", nullable: false),
                    Teachingclass = table.Column<string>(type: "CHAR(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => new { x.Sno, x.Cno });
                    table.ForeignKey(
                        name: "FK_Claims_Course_Cno",
                        column: x => x.Cno,
                        principalTable: "Course",
                        principalColumn: "Cno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Student_Sno",
                        column: x => x.Sno,
                        principalTable: "Student",
                        principalColumn: "Sno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_Cno",
                table: "Claims",
                column: "Cno");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Cpno",
                table: "Course",
                column: "Cpno");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Sname",
                table: "Student",
                column: "Sname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
