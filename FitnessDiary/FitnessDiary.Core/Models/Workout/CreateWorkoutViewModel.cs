using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Workout
{
    public class CreateWorkoutViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [StringLength(250, MinimumLength = 5)]
        public string? Description { get; set; }
        public ExerciseViewModel[] Exercises { get; set; } = null!;
    }
}
