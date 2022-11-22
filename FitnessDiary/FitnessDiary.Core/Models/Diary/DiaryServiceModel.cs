using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Models.Diary
{
    public class DiaryServiceModel
    {
        public DiaryDayServiceModel CurrentDay { get; set; } = null!;
        public IList<DiaryDayServiceModel> DiaryDays { get; set; } = null!;
    }
}
