using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Core.Models.Article
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(80,MinimumLength = 4)]
        public string Name { get; set; } = null!;
    }
}
