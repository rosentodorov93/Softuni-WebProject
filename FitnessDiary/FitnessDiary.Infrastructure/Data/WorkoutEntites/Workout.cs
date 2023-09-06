using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.WorkoutTamplateDataConstants;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class Workout
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }
        public IList<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
