using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [MaxLength(450)]
        public string? ImageUrl { get; set; } 

        [Required]
        [MaxLength(30)]
        public string Author { get; set; } = null!;

        [Required]
        public string Category { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;
    }
}
