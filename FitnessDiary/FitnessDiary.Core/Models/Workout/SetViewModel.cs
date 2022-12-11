using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Workout
{
    public class SetViewModel
    {
        [Range(1,100)]
        public int Reps { get; set; }
        [Range(typeof(double), "0.0", "1000.0")]
        public double Load { get; set; }
    }
}