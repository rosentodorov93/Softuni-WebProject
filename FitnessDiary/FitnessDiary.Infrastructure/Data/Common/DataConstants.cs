namespace FitnessDiary.Infrastructure.Data.Common
{
    public static class DataConstants
    {
        public static class FoodDataConstants
        {
            public const int MaxNameLength = 50;
            public const int MinNameLength = 4;

            public const int MaxTypeLength = 30;
            public const int MinTypeLength = 3;
        }

        public static class NutritionDataConstants
        {
            public const string MaxNutritionMetric = "1.7976931348623157E+308";
            public const string MinNutritionMetric = "0.1";

        }

        public static class RecipeDataConstants
        {
            public const int MaxNameLength = 50;
            public const int MinNameLength = 4;


            public const int MaxServingSize = int.MaxValue;
            public const int MinServingSize = 1;

            public const string AmountMinValue = "0.1"
            public const string AmontMaxValue = "1.7976931348623157E+308";
        }

    }
}
