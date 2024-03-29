﻿using FitnessDiary.Core.Contracts;
using FitnessDiary.Core.Models.Account;
using FitnessDiary.Core.Models.Diary;
using FitnessDiary.Infrastructure.Data;
using FitnessDiary.Infrastructure.Data.Account;
using FitnessDiary.Infrastructure.Data.Common;
using FitnessDiary.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessDiary.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository repo;
        private readonly UserManager<IdentityUser> userManager;

        public AccountService(IRepository _repo, UserManager<IdentityUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }
        /// <summary>
        /// Creates new application user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="age"></param>
        /// <param name="fullName"></param>
        /// <param name="gender"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="activityLevel"></param>
        /// <param name="fitnessGoal"></param>
        /// <returns></returns>
        public async Task<bool> CreateApplicationUser(IdentityUser user,
            int age,
            string fullName,
            int gender,
            int height,
            double weight,
            int activityLevel,
            int fitnessGoal)
        {
            var applicationUser = new ApplicationUser()
            {
                FullName = fullName,
                Gender = (Gender)gender,
                Age = age,
                Height = height,
                Weight = weight,
                ActivityLevelId = activityLevel,
                FitnessGoal = (FitnessGoalType)fitnessGoal,
                TargetNutrients = new NutritionData(),
                Diary = new List<DiaryDay>(),
                User = user
            };
            var targetNutrient = await CalculateTargetNutrientsAsync(applicationUser);
            applicationUser.TargetNutrients.Calories = targetNutrient.Calories;
            applicationUser.TargetNutrients.Carbohydrates = targetNutrient.Carbs;
            applicationUser.TargetNutrients.Proteins = targetNutrient.Protein;
            applicationUser.TargetNutrients.Fats = targetNutrient.Fats;

            await repo.AddAsync(applicationUser);
            await repo.SaveChangesAsync();

            return true;
        }

        private async Task<NutritionServiceModel> CalculateTargetNutrientsAsync(ApplicationUser user)
        {
            double targetCalories = 0;

            if (user.Gender == Gender.Male)
            {
                targetCalories = 66.5 + (13.75 * user.Weight) + (5.003 * user.Height) - (6.75 * user.Age);
            }
            else
            {
                targetCalories = 655.1 + (9.563 * user.Weight) + (1.850 * user.Height) - (4.676 * user.Age);
            }
            var activityLevelValue = await repo.AllReadonly<ActivityLevel>().FirstOrDefaultAsync(a => a.Id == user.ActivityLevelId);
            targetCalories *= activityLevelValue.Value;

            if (user.FitnessGoal == FitnessGoalType.GainWeight)
            {
                targetCalories += 500;
            }
            else if (user.FitnessGoal == FitnessGoalType.LoseWeight)
            {
                targetCalories -= 500;
            }

            var targetCarbs = ((user.CarbsPercent * targetCalories / 100)) / 4;
            var targetProtein = ((user.ProteinPercent * targetCalories / 100)) / 4;
            var targetFats = ((user.FatsPercent * targetCalories / 100)) / 9;

            return new NutritionServiceModel
            {
                Calories = targetCalories,
                Carbs = targetCarbs,
                Protein = targetProtein,
                Fats = targetFats
            };
        }
        /// <summary>
        /// Creates new administration user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateAdministrationUser(CreateAdministrationUserViewModel model)
        {
            var username = $"{model.FirstName.Substring(0, 1)}.{model.LastName}";
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = username
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, model.RoleName);
                var administrationUser = new AdministrationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    User = user,
                };
                await repo.AddAsync(administrationUser);
                await repo.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Checks if application user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<bool> ExistsById(string? userId)
        {
            return repo.AllReadonly<ApplicationUser>().AnyAsync(u => u.Id == userId);
        }
        /// <summary>
        /// Returns all activity levels
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ActivityLevel>> GetActivityLevels()
            => await repo.All<ActivityLevel>().ToListAsync();
        /// <summary>
        /// Returns all users in the system
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync()
        {
            var users = new List<AllUsersViewModel>();
            var appUsers = await this.repo.All<ApplicationUser>().Select(u => new AllUsersViewModel()
            {
                Id = u.User.Id,
                Username = u.User.UserName,
                Email = u.User.Email,
                AccountType = "Application",
            }).ToListAsync();

            var adminUsers = await this.repo.All<AdministrationUser>().Select(u => new AllUsersViewModel()
            {
                Id = u.User.Id,
                Username = u.User.UserName,
                Email = u.User.Email,
                AccountType = "Administration",
            }).ToListAsync();

            users.AddRange(appUsers);
            users.AddRange(adminUsers);

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(repo.All<IdentityUser>().First(u => u.Id == user.Id));

                user.Role = String.Join(", ", roles);
            }
      
            return users;
        }
        /// <summary>
        /// Returns user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string? GetById(string id)
        {
            var appUser = repo.AllReadonly<ApplicationUser>().FirstOrDefault(a => a.UserId == id);

            return appUser?.Id ?? null;
        }
        /// <summary>
        /// Returns required nutrients for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<NutritionServiceModel> GetUserTargetNutritionAsync(string userId)
        {
            var user = await repo
                .All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.TargetNutrients).FirstOrDefaultAsync();

            return new NutritionServiceModel()
            {
                Calories = user.TargetNutrients.Calories,
                Carbs = user.TargetNutrients.Carbohydrates,
                Protein = user.TargetNutrients.Proteins,
                Fats = user.TargetNutrients.Fats
            };
        }
        /// <summary>
        /// Returns appuser full name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetAppUserFullName(string userId)
        {
            var fullName = repo.AllReadonly<ApplicationUser>().Where(u => u.UserId == userId).FirstOrDefault().FullName;

            if (string.IsNullOrEmpty(fullName))
            {
                return null;
            }

            return fullName;
        }
        /// <summary>
        /// Returns Administration user full name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetAdminUserFullName(string userId)
        {
            var user = repo.AllReadonly<AdministrationUser>().Where(u => u.UserId == userId).FirstOrDefault();

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
