using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FitnessDiary.Core.Models.Article
{
    public class ArticleDetailsViewModel
    {
        public string Id { get; set; } = null!;
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
