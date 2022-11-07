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
    public class Food
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(25)]
        public MeassureUnitType MeassureUnits { get; set; } 

        [Required]
        [MaxLength(30)]
        public string Type { get; set; } = null!;

        [Required]
        public int NutritionId { get; set; }

        [ForeignKey(nameof(NutritionId))]
        public NutritionData Nutrition { get; set; } = null!;

    }
}
