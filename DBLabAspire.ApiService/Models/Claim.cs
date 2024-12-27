using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBLabAspire.ApiService.Models
{
    public record Claims
    {
        [Key]
        [Column("Sno", TypeName = "CHAR(8)", Order = 0)]
        public virtual string StudentId { get; set; }

        [Key]
        [Column("Cno", TypeName = "CHAR(5)", Order = 1)]
        public virtual string CourseId { get; set; }

        [Column("Grade")]
        public virtual short? Grade { get; set; }

        [Column("Semester", TypeName = "CHAR(5)")]
        public virtual string Semester { get; set; }

        [Column("Teachingclass", TypeName = "CHAR(8)")]
        public virtual string TeachingClass { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
