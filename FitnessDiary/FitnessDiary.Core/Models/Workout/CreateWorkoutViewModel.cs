using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Workout
{
    public class CreateWorkoutViewModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ExerciseViewModel[] Exercises { get; set; } = null!;
    }
}
