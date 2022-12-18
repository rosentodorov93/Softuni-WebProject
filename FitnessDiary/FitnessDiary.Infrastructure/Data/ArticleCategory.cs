using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Infrastructure.Data
{
    public class ArticleCategory
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(80)]
        public string Name { get; set; } = null!;
    }
}
