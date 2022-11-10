using FitnessDiary.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Food
{
    public class MinePageViewModel
    {
        public IEnumerable<string> Types { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string SearchTerm { get; set; } = null!;
        public FoodSorting Sorting { get; set; } 
        public int TotalFoods { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PreviousPage => CurrentPage - 1;
        public double MaxPage => Math.Ceiling((double)TotalFoods / FoodsPerPage);

        public  int FoodsPerPage  = 2;
        public IEnumerable<FoodQueryModel> Foods { get; set; }
    }
}
