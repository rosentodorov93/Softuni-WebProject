using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FitnessDiary.Core.Models.Article
{
    public class AddViewModel
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
