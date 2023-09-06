using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ServingDataConstants;

namespace FitnessDiary.Infrastructure.Data
{
    public class Serving
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(typeof(double), MinAmount, MaxAmount)]
        public double Amount { get; set; }

        [Required]
        public ServingCategory Category { get; set; }

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData Nutrition { get; set; } = null!;
        public int DiaryDayId { get; set; }

        [ForeignKey(nameof(DiaryDayId))]
        public DiaryDay DiaryDay { get; set; } = null!;

    }
}
