using FitnessDiary.Infrastructure.Data.WorkoutEntites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data
{
    public class DiaryDay
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public IList<Serving> Servings { get; set; } = new List<Serving>();

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData Nutrition { get; set; } = null!;
        public string? WorkoutId { get; set; }

        [ForeignKey(nameof(WorkoutId))]
        public Workout? Workout { get; set; }
    }
}
