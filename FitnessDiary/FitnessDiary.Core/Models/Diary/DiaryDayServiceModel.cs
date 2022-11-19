using FitnessDiary.Core.Models.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Diary
{
    public class DiaryDayServiceModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public IList<ServingViewModel> BreakfastServings { get; set; } = new List<ServingViewModel>();
        public IList<ServingViewModel> LunchServings { get; set; } = new List<ServingViewModel>();
        public IList<ServingViewModel> DinnerServings { get; set; } = new List<ServingViewModel>();
        public IList<ServingViewModel> SnackServings { get; set; } = new List<ServingViewModel>();
        public NutritionServiceModel Nutrition { get; set; } = null!;
        public AddToDiaryViewModel? Workout { get; set; }
    }
}
