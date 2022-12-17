using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ExerciseDataConstants;

namespace FitnessDiary.Core.Models.Workout
{
    public class AddExerciseModel
    {
        public string WorkoutId { get; set; } = null!;
        [Required]
        [StringLength(MaxNameLength,MinimumLength = MinNameLength)]
        public string ExerciseName { get; set; } = null!;
        public string BodyPart { get; set; } = null!;

        [Range(MinSetCount, MaxSetCount)]
        public int SetCount { get; set; } 
    }
}
