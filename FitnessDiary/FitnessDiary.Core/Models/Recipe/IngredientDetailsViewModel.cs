﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Recepie
{
    public class IngredientDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double  Amount { get; set; }
    }
}