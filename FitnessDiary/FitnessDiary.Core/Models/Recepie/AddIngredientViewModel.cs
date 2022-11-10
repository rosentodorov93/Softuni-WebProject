using FitnessDiary.Core.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class AddIngredientViewModel
    {
        public string RecepieId { get; set; }
        public IEnumerable<FoodQueryModel> Foods { get; set; }
        public IngredientViewModel Ingredient { get; set; }
    }
}
