using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Diary
{
    public class AddServingViewModel
    {
        public List<FoodDiaryServiceModel> Foods { get; set; }
        public ServingServiceModel Serving { get; set; }
    }
}
