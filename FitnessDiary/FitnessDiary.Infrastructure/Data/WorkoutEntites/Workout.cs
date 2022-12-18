using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class Workout
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }
        public IList<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
