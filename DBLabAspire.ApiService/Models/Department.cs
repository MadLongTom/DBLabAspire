using System.ComponentModel.DataAnnotations;

namespace DBLabAspire.ApiService.Models
{
    public record Department
    {
        [Key]
        public virtual Guid DepartNo { get; set; }
        public virtual string DepartName { get; set; } = string.Empty;
    }
}
