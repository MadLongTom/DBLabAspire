using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBLabAspire.ApiService.Models
{
    [Table("Course")]
    public record Course
    {
        [Key]
        [Column("Cno", TypeName = "CHAR(5)")]
        public virtual string Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Column("Cname", TypeName = "VARCHAR(40)")]
        public virtual string Name { get; set; }

        [Column("Ccredit")]
        public virtual short Credit { get; set; }

        [Column("Cpno", TypeName = "CHAR(5)")]
        public virtual string? PrerequisiteCourseId { get; set; }

        public virtual string TeacherName { get; set; } = string.Empty;

        [ForeignKey("PrerequisiteCourseId")]
        public virtual Course? PrerequisiteCourse { get; set; }
    }
}
