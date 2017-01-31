﻿using System.Collections.Generic;

using WhenItsDone.DTOs.DishViews;
using WhenItsDone.Models;

namespace WhenItsDone.Services.Contracts
{
    public interface IDishesAsyncService : IGenericAsyncService<Dish>, IService
    {
        IEnumerable<NamePhotoDishView> GetTopCountDishesByRating(int dishesCount, bool addSampleData);
    }
}
