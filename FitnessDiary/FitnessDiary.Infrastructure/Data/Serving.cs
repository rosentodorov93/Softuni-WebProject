using FitnessDiary.Infrastructure.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data
{
    public class Serving
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public double Amount { get; set; }

        [Required]
        public ServingCategory Category { get; set; }

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData Nutrition { get; set; } = null!;
        public int DiaryDayId { get; set; }

        [ForeignKey(nameof(DiaryDayId))]
        public DiaryDay DiaryDay { get; set; } = null!;

    }
}
