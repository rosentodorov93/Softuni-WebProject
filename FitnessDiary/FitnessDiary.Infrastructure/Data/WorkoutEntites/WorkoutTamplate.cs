using FitnessDiary.Infrastructure.Data.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class WorkoutTamplate
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }
        public IList<ExerciseTamplate> Exercises { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
