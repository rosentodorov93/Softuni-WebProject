using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ActivityLevelDataConstants;

namespace FitnessDiary.Infrastructure.Data
{
    public class ActivityLevel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTypeLength, MinimumLength = MinTypeLength)]
        public string Type { get; set; } = null!;

        [Required]
        public double Value { get; set; }
    }
}