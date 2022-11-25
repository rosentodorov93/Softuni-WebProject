using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class DetailsViewModel: RecipeListingViewModel
    {
        public double Carbs { get; set; }
        public List<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
        public double Protein { get; set; }
        public double Fats { get; set; }
    }
}
