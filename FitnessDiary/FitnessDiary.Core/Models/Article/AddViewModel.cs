using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ArticleDataConstants;

namespace FitnessDiary.Core.Models.Article
{
    public class AddViewModel
    {
        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(MaxAuthorLength, MinimumLength = MinAuthorLength)]
        public string Author { get; set; } = null!;

        [Required]
        public string CategoryId { get; set; } = null!;

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; } = null!;

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
