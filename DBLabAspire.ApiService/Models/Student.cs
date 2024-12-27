using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBLabAspire.ApiService.Models
{
    [Table("Student")]
    public record Student
    {
        [Key]
        [Column("Sno", TypeName = "CHAR(8)")]
        public virtual string Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Sname", TypeName = "VARCHAR(20)")]
        public virtual string Name { get; set; }

        [Required]
        [MaxLength(6)]
        [Column("Ssex", TypeName = "CHAR(6)")]
        public virtual string Sex { get; set; }

        [Column("Sbirthdate")]
        public virtual DateTimeOffset BirthDate { get; set; }

        [MaxLength(40)]
        [Column("Smajor", TypeName = "VARCHAR(40)")]
        public virtual string Major { get; set; } = string.Empty;

        [ForeignKey("DepartNo")]
        public virtual Department? Department { get; set; }
    }
}
