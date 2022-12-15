using FitnessDiary.Core.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class AddIngredientViewModel : AddIngredientInputModel
    {
        public IEnumerable<FoodServiceModel>? Foods { get; set; }
    }
}
