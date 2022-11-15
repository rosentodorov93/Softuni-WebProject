using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class Workout
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string? Description { get; set; }
        public IList<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
