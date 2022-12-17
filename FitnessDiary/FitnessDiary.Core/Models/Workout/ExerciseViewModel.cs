using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ExerciseDataConstants;

namespace FitnessDiary.Core.Models.Workout
{
    public class ExerciseViewModel
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        public string BodyPart { get; set; } = null!;

        [Range(MinSetCount, MaxSetCount)]
        public int SetCount { get; set; }
    }
}
