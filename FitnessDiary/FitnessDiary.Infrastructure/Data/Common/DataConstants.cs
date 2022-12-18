namespace FitnessDiary.Infrastructure.Data.Common
{
    public static class DataConstants
    {
        public static class AdministrationUserDataConstants
        {
            public const int MaxFirstNameLength = 80;
            public const int MinFirstNameLength = 4;

            public const int MaxLastNameLength = 80;
            public const int MinLastNameLength = 4;
        }

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

            public const string AmountMinValue = "0.1";
            public const string AmontMaxValue = "100";
        }

        public static class WorkoutTamplateDataConstants
        {
            public const int MaxNameLength = 80;
            public const int MinNameLength = 4;

            public const int MaxDescriptionLength = 250;
            public const int MinDescriptionLength = 5;

            public const string InvalidTamplateId = "Invalid workout tamplate Id";

        }

        public static class ExerciseDataConstants
        {
            public const int MaxNameLength = 70;
            public const int MinNameLength = 3;

            public const int MaxSetCount = 100;
            public const int MinSetCount = 1;
        }

        public static class SetDataConstants
        {
            public const int MaxRepsCount = 100;
            public const int MinRepsCount = 1;

            public const string MinLoad = "0.1";
            public const string MaxLoad= "1000";
        }

        public static class ServingDataConstants
        {
            public const int MaxAmount = 100;
            public const int MinAmount = 1;

         
        }

        public static class ArticleDataConstants
        {
            public const int MaxTitleLength = 50;
            public const int MinTitleLength = 5;

            public const int MaxAuthorLength = 30;
            public const int MinAuthorLength = 3;

        }

        public static class ArticleCategoryDataConstants
        {
            public const int MaxNameLength = 80;
            public const int MinNameLength = 4;
        }

        public static class ApplicationUserDataConstants
        {
            public const int MaxFullNameLength = 80;
            public const int MinFullNameLength = 4;

            public const int MaxAge = 110;
            public const int MinAge = 1;

            public const int MaxHeight = 250;
            public const int MinHeight = 1;

            public const string MaxWeight = "500";
            public const string MinWeight = "1";

            public const string InvalidUserId = "Invalid user Id";
        }
    }
}
