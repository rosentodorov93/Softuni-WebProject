using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ArticleCategoryDataConstants;

namespace FitnessDiary.Infrastructure.Data
{
    public class ArticleCategory
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;
    }
}
