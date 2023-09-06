using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ExerciseDataConstants;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class Exercise
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        public BodyPartType BodyPart { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();
    }
}
