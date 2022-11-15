using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class Set
    {
        [Key]
        public int Id { get; set; }
        public int Reps { get; set; }
        public double Load { get; set; }

        [Required]
        public string ExerciseId { get; set; } = null!;

        [ForeignKey(nameof(ExerciseId))]
        public Exercise Exercise { get; set; } = null!;
    }
}
