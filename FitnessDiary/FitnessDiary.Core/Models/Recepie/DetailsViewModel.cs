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
        public double Protein { get; set; }
        public double Fats { get; set; }
        public bool isFinished { get; set; }
    }
}
