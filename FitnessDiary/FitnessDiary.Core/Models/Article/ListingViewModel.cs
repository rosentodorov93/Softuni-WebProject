using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Article
{
    public class ListingViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; } 
        public string Author { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}
