using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data
{
    public class FitnessGoal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Type { get; set; } = null!;
    }
}
