using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Statistics
{
    public class StatisticsServiceModel
    {
        public string[] Labels { get; set; } = new string[7];
        public double[] Calories { get; set; } = new double[7];
        public double[] Workouts { get; set; } = new double[7];
    }
}
