using FitnessDiary.Core.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class IngredientViewModel
    {
        public string FoodId { get; set; } = null!;

        public int Amount { get; set; }

    }
}
