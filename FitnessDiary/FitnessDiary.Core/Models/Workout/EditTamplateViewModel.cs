using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Workout
{
    public class EditTamplateViewModel
    {
        public string Id { get; set; } = null!;
        [Required]
        [StringLength(80,MinimumLength = 5)]
        public string Name { get; set; } = null!;

        [StringLength(250,MinimumLength = 5)]
        public string? Description { get; set; }
        public List<EditExerciseViewModel> Exercises { get; set; } = null!;
    }
}
