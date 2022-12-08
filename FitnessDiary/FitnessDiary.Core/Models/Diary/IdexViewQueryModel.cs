using FitnessDiary.Core.Models.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Diary
{
    public class IdexViewQueryModel
    {
        public DateTime CurrentDayDate { get; set; }
        public IList<ServingViewModel> Breakfast { get; set; } = new List<ServingViewModel>();
        public IList<ServingViewModel> Lunch { get; set; } = new List<ServingViewModel>();
        public IList<ServingViewModel> Dinner { get; set; } = new List<ServingViewModel>();
        public IList<ServingViewModel> Snack { get; set; } = new List<ServingViewModel>();
        public NutritionStatisticsViewModel NutritionStatistics { get; set; } = null!;
        public WorkoutViewModel Workout { get; set; } = null!;
    }
}
