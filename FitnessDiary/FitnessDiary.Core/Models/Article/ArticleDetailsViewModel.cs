using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FitnessDiary.Core.Models.Article
{
    public class ArticleDetailsViewModel: AddViewModel
    {
        public string Id { get; set; } = null!;

    }
}
