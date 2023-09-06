using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ExerciseDataConstants;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class ExerciseTamplate
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [Required]
        public BodyPartType BodyPart { get; set; }
        public int SetCount { get; set; }
    }
}
