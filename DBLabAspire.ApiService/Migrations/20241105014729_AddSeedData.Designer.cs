﻿// <auto-generated />
using System;
using DBLabAspire.ApiService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DBLabAspire.ApiService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241105014729_AddSeedData")]
    partial class AddSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DBLabAspire.ApiService.Models.Claims", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("CHAR(8)")
                        .HasColumnName("Sno")
                        .HasColumnOrder(0);

                    b.Property<string>("CourseId")
                        .HasColumnType("CHAR(5)")
                        .HasColumnName("Cno")
                        .HasColumnOrder(1);

                    b.Property<short?>("Grade")
                        .HasColumnType("smallint")
                        .HasColumnName("Grade");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("CHAR(5)")
                        .HasColumnName("Semester");

                    b.Property<string>("TeachingClass")
                        .IsRequired()
                        .HasColumnType("CHAR(8)")
                        .HasColumnName("Teachingclass");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Claims");

                    b.HasData(
                        new
                        {
                            StudentId = "S000001",
                            CourseId = "C001",
                            Grade = (short)85,
                            Semester = "2021F",
                            TeachingClass = "TC001"
                        },
                        new
                        {
                            StudentId = "S000001",
                            CourseId = "C002",
                            Grade = (short)90,
                            Semester = "2021F",
                            TeachingClass = "TC002"
                        },
                        new
                        {
                            StudentId = "S000002",
                            CourseId = "C002",
                            Grade = (short)75,
                            Semester = "2021F",
                            TeachingClass = "TC002"
                        },
                        new
                        {
                            StudentId = "S000003",
                            CourseId = "C003",
                            Grade = (short)80,
                            Semester = "2021F",
                            TeachingClass = "TC003"
                        });
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("CHAR(5)")
                        .HasColumnName("Cno");

                    b.Property<short>("Credit")
                        .HasColumnType("smallint")
                        .HasColumnName("Ccredit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("VARCHAR(40)")
                        .HasColumnName("Cname");

                    b.Property<string>("PrerequisiteCourseId")
                        .HasColumnType("CHAR(5)")
                        .HasColumnName("Cpno");

                    b.HasKey("Id");

                    b.HasIndex("PrerequisiteCourseId");

                    b.ToTable("Course");

                    b.HasData(
                        new
                        {
                            Id = "C001",
                            Credit = (short)3,
                            Name = "Introduction to Computer Science"
                        },
                        new
                        {
                            Id = "C002",
                            Credit = (short)4,
                            Name = "Calculus I"
                        },
                        new
                        {
                            Id = "C003",
                            Credit = (short)4,
                            Name = "Physics I"
                        });
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.Student", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("CHAR(8)")
                        .HasColumnName("Sno");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Sbirthdate");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("VARCHAR(40)")
                        .HasColumnName("Smajor");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("Sname");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("CHAR(6)")
                        .HasColumnName("Ssex");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Student");

                    b.HasData(
                        new
                        {
                            Id = "S000001",
                            BirthDate = new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Major = "CS",
                            Name = "Alice",
                            Sex = "FM"
                        },
                        new
                        {
                            Id = "S000002",
                            BirthDate = new DateTimeOffset(new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Major = "CS",
                            Name = "Bob",
                            Sex = "M"
                        },
                        new
                        {
                            Id = "S000003",
                            BirthDate = new DateTimeOffset(new DateTime(2000, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)),
                            Major = "Physics",
                            Name = "Charlie",
                            Sex = "Male"
                        });
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.View.HighestAverageGradeCourse", b =>
                {
                    b.Property<string>("Cno")
                        .HasColumnType("text");

                    b.Property<string>("Cname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("averagegrade")
                        .HasColumnType("double precision");

                    b.HasKey("Cno");

                    b.ToTable((string)null);

                    b.ToView("view_highestaveragegradecourse", (string)null);
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.View.HighestAverageGradeCourseClaims", b =>
                {
                    b.Property<string>("Sno")
                        .HasColumnType("text");

                    b.Property<string>("Cname")
                        .HasColumnType("text");

                    b.Property<short>("Grade")
                        .HasColumnType("smallint");

                    b.Property<string>("Sname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Sno", "Cname");

                    b.ToTable((string)null);

                    b.ToView("view_highestaveragegradecourseclaims", (string)null);
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.View.HighestAverageGradeStudentDepartment", b =>
                {
                    b.Property<string>("Smajor")
                        .HasColumnType("text");

                    b.Property<double>("averagegrade")
                        .HasColumnType("double precision");

                    b.HasKey("Smajor");

                    b.ToTable((string)null);

                    b.ToView("view_highestaveragegradestudentdepartment", (string)null);
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.Claims", b =>
                {
                    b.HasOne("DBLabAspire.ApiService.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DBLabAspire.ApiService.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DBLabAspire.ApiService.Models.Course", b =>
                {
                    b.HasOne("DBLabAspire.ApiService.Models.Course", "PrerequisiteCourse")
                        .WithMany()
                        .HasForeignKey("PrerequisiteCourseId");

                    b.Navigation("PrerequisiteCourse");
                });
#pragma warning restore 612, 618
        }
    }
}
