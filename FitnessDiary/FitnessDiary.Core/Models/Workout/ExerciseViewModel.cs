using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Workout
{
    public class ExerciseViewModel
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        public string BodyPart { get; set; } = null!;
        public int SetCount { get; set; }
    }
}
