﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class RemoveIngredientViewModel
    {
        public IEnumerable<IngredientDetailsViewModel> Ingredients { get; set; }
        public string Recipeid { get; set; }
        public int IngredientToRemove { get; set; }
    }
}
