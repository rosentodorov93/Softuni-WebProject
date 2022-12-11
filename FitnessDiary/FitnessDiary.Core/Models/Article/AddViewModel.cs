using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FitnessDiary.Core.Models.Article
{
    public class AddViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
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
