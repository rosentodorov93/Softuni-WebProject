using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.WorkoutTamplateDataConstants;

namespace FitnessDiary.Core.Models.Workout
{
    public class CreateWorkoutViewModel
    {
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
        public string? Description { get; set; }
        public ExerciseViewModel[] Exercises { get; set; } = null!;
    }
}
