using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ArticleDataConstants;

namespace FitnessDiary.Infrastructure.Data
{
    public class Article
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(MaxAuthorLength, MinimumLength = MinAuthorLength)]
        public string Author { get; set; } = null!;

        [Required]
        public string CategoryId { get; set; } = null!;

        [ForeignKey(nameof(CategoryId))]
        public ArticleCategory Category { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
