using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FitnessDiary.Core.Models.Article
{
    public class AddViewModel
    {
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime Date { get; set; } 
        public string Author { get; set; } = null!;
        public string Category { get; set; } = null!;
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; } = null!;
    }
}
