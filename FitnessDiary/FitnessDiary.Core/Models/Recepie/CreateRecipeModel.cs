using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class CreateRecipeModel
    {
        public string Name { get; set; } = null!;
        public int ServingsSize { get; set; }
        public string UserId { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
    }
}
