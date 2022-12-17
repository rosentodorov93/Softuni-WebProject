using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.ServingDataConstants;

namespace FitnessDiary.Core.Models.Diary
{
    public class ServingServiceModel
    {
        [Required]
        public string Id { get; set; } = null!;
        public string Category { get; set; } = null!;

        [Range(MinAmount,MaxAmount)]
        public double Amount { get; set; } 
    }
}