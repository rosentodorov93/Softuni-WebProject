using FitnessDiary.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class ExerciseTamplate
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(70)]
        public string Name { get; set; } = null!;

        [Required]
        public BodyPartType BodyPart { get; set; }
        public int SetCount { get; set; }
    }
}
