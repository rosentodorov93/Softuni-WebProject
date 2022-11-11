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
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
