using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class EditViewModel : CreateViewModel
    {
        public string Id { get; set; } = null!;
        public List<IngredientDetailsViewModel> Ingredients { get; set; } = null!;
    }
}
