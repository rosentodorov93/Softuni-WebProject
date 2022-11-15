using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Workout
{
    public class ExerciseViewModel
    {
        public string Name { get; set; }
        public string BodyPart { get; set; }
        public int SetCount { get; set; }
    }
}
