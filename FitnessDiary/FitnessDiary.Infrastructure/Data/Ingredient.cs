using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FoodId { get; set; } = null!;

        [ForeignKey(nameof(FoodId))]
        public Food Food { get; set; } = null!;
        public double Amount { get; set; }

    }
}
