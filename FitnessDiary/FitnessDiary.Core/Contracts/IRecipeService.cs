﻿using FitnessDiary.Core.Models.Food;
using FitnessDiary.Core.Models.Recepie;
using FitnessDiary.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessDiary.Core.Contracts
{
    public interface IRecipeService
    {
        Task AddAsync(CreateViewModel model);
        Task<DetailsViewModel> AddIngredientAsync(IngredientViewModel ingredient, int recepieId);
    }
}
