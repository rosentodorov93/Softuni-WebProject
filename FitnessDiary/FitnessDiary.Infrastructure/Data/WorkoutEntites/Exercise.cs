using FitnessDiary.Infrastructure.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data.WorkoutEntites
{
    public class Exercise
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(70)]
        public string Name { get; set; } = null!;

        [Required]
        public BodyPartType BodyPart { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();
    }
}
