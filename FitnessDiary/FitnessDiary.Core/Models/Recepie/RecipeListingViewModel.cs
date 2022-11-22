using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class RecipeListingViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ServingsSize { get; set; }
        public int Unit { get; set; }
        public double TotalCalories { get; set; }
        public double CaloriesPerPortion { get; set; }
    }
}
