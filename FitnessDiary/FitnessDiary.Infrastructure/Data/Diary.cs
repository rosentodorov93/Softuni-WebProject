using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Infrastructure.Data
{
    public class Diary
    {
        [Key]
        public int Id { get; set; }
        public IList<DiaryDay> DiaryDays { get; set; } = new List<DiaryDay>();

        [Required]
        public int DiaryDayId { get; set; }

        [ForeignKey(nameof(DiaryDayId))]
        public DiaryDay CurrentDiaryDay { get; set; } = null!;
    }
}
