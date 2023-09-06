using FitnessDiary.Infrastructure.Data.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.WorkoutTamplateDataConstants;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class WorkoutTamplate
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }
        public IList<ExerciseTamplate> Exercises { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
