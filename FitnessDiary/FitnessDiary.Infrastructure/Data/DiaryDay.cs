using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data
{
    public class DiaryDay
    {
        [Key]
        public int Id { get; set; }
        public  DateTime DateTime { get; set; }
        public IList<Serving> Servings { get; set; } = new List<Serving>();

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData Nutrition { get; set; } = null!;
    }
}
