using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Workout
{
    public class RemoveExerciseModel
    {
        public string ExerciseId { get; set; } = null!;
        public string TamplateId { get; set; } = null!;
    }
}
