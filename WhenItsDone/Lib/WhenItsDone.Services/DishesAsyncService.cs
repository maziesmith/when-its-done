﻿using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using WhenItsDone.Data.Contracts;
using WhenItsDone.Data.UnitsOfWork.Factories;
using WhenItsDone.DTOs.DishViewsDTOs;
using WhenItsDone.Models;
using WhenItsDone.Services.Abstraction;
using WhenItsDone.Services.Contracts;
using WhenItsDone.Models.Factories;

namespace WhenItsDone.Services
{
    public class DishesAsyncService : GenericAsyncService<Dish>, IDishesAsyncService, IGenericAsyncService<Dish>, IService
    {
        private readonly IDishesAsyncRepository dishesAsyncRepository;
        private readonly IUsersAsyncRepository usersAsyncRepository;
        private readonly IInitializedDishFactory dishFactory;

        public DishesAsyncService(IDishesAsyncRepository dishesAsyncRepository, IUsersAsyncRepository usersAsyncRepository, IInitializedDishFactory dishFactory, IDisposableUnitOfWorkFactory unitOfWorkFactory)
            : base(dishesAsyncRepository, unitOfWorkFactory)
        {
            Guard.WhenArgument(dishesAsyncRepository, nameof(IDishesAsyncRepository)).IsNull().Throw();
            Guard.WhenArgument(usersAsyncRepository, nameof(IUsersAsyncRepository)).IsNull().Throw();
            Guard.WhenArgument(dishFactory, nameof(IDishFactory)).IsNull().Throw();

            this.dishesAsyncRepository = dishesAsyncRepository;
            this.usersAsyncRepository = usersAsyncRepository;
            this.dishFactory = dishFactory;
        }

        public IEnumerable<NamePhotoRatingDishViewDTO> GetTopCountDishesByRating(int dishesCount, bool addSampleData)
        {
            if (dishesCount < 0)
            {
                throw new ArgumentException("dishesCount parameter must be greater than or equal to 0.");
            }

            var topDishes = this.dishesAsyncRepository.GetTopCountDishesByRating(dishesCount).Result;
            if (topDishes.Count < dishesCount && addSampleData == true)
            {
                topDishes = this.dishesAsyncRepository.AddTopCountDishesSampleData(dishesCount, topDishes);
            }

            return topDishes;
        }

        public bool CreateDish(string username, string dishName, string price, string calories, string carbohydrates, string fats, string protein, string video)
        {
            var isSuccessful = false;
            if (string.IsNullOrEmpty(username))
            {
                return isSuccessful;
            }

            var loggedUserId = this.usersAsyncRepository.GetCurrentUserId(username);
            if (!loggedUserId.HasValue)
            {
                return isSuccessful;
            }

            var convertedPrice = this.ConvertStringValueToDecimal(price);
            var convertedCalories = this.ConvertStringValueToDecimal(calories);
            var convertedCarbohydrates = this.ConvertStringValueToDecimal(carbohydrates);
            var convertedFats = this.ConvertStringValueToDecimal(fats);
            var convertedProtein = this.ConvertStringValueToDecimal(protein);

            var newDish = this.dishFactory.GetInitializedDish(dishName, convertedPrice, convertedCalories, convertedCarbohydrates, convertedFats, convertedProtein);
            this.dishesAsyncRepository.Add(newDish);
            using (var unitOfWork = base.UnitOfWorkFactory.CreateUnitOfWork())
            {
                var result = unitOfWork.SaveChanges();
                if (result != 0)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        private decimal ConvertStringValueToDecimal(string value)
        {
            decimal convertedValue;
            if (!decimal.TryParse(value, out convertedValue))
            {
                throw new ArgumentException("Invalid decimal value");
            }

            return convertedValue;
        }
    }
}
