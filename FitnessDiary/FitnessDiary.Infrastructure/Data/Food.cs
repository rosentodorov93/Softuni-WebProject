using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.FoodDataConstants;

namespace FitnessDiary.Infrastructure.Data
{
    public class Food
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(25)]
        public MeassureUnitType MeassureUnits { get; set; }

        [Required]
        [StringLength(MaxTypeLength, MinimumLength = MinTypeLength)]
        public string Type { get; set; } = null!;

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData Nutrition { get; set; } = null!;

        [MaxLength(450)]
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
