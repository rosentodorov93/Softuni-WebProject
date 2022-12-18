using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessDiary.Infrastructure.Data
{
    public class Article
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(30)]
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
