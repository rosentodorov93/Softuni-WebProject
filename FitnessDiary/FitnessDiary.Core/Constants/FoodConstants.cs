namespace FitnessDiary.Core.Constants
{
    public static class FoodConstants
    {
        public const string AllFoodsCacheKey = "AllFoodsCacheKey";
        public const string InvalidFoodId = "Invalid food Id";
        public const string FoodDoesNotExist = "Food does not exist";
        public const string AdminPrivateFoodError = "Admin can't edit private foods";
        public const string AdminPrivateFoodLogError = "Admin {0} don't have access to food with id {1}";
        public const string ModeratorPrivateFoodError = "Moderator can't edit private foods";
        public const string ModeratorPrivateFoodLogError = "Moderator {0} don't have access to food with id {1}";
        public const string UserPublicFoodError = "You can't edit public food database";
        public const string UserPublicFoodLogError = "User {0} don't have access to food with id {1}";
    }
}
