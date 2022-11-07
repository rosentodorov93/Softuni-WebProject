using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class EditViewModel : CreateViewModel
    {
        public List<IngredientDetailsViewModel> Ingredients { get; set; }
    }
}
