using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.SetDataConstants;

namespace FitnessDiary.Core.Models.Workout
{
    public class SetViewModel
    {
        [Range(MinRepsCount,MaxRepsCount)]
        public int Reps { get; set; }
        [Range(typeof(double), MinLoad, MaxLoad)]
        public double Load { get; set; }
    }
}