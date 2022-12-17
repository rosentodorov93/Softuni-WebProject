using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ArticleCategoryDataConstants;

namespace FitnessDiary.Core.Models.Article
{
    public class CategoryViewModel 
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

    }
}
