﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Workout
{
    public class AddToDiaryViewModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<ExerciseWithSetsViewModel> Exercises { get; set; } = null!;
    }
}
