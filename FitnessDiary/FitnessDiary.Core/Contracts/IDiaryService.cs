using FitnessDiary.Core.Models.Diary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IDiaryService
    {
        Task<DiaryDayServiceModel> GetByIdAsync(string userId);
    }
}
