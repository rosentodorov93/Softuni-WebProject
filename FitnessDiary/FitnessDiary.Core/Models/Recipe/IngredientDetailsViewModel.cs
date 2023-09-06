using System.ComponentModel.DataAnnotations;
using static FitnessDiary.Infrastructure.Data.Common.DataConstants.IgredientsDataConstants;

namespace FitnessDiary.Core.Models.Recepie
{

    public class IngredientDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [Range(typeof(double), AmountMinValue, AmontMaxValue)]
        public double Amount { get; set; }
    }
}
