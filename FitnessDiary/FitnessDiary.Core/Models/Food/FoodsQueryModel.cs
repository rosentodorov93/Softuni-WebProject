using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Food
{
    public class FoodsQueryModel
    {
        public int TotalFoodsCount { get; set; }
        public IEnumerable<FoodServiceModel> Foods { get; set; }
    }
}
