using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Infrastructure.Data
{
    public class ActivityLevel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Type { get; set; } = null!;

        [Required]
        public double Value { get; set; }
    }
}