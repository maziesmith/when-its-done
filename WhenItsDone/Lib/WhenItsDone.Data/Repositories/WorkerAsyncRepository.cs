﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhenItsDone.Data.Contracts;
using WhenItsDone.DTOs.WorkerVIewsDTOs;
using WhenItsDone.Models;

namespace WhenItsDone.Data.Repositories
{
    public class WorkerAsyncRepository : GenericAsyncRepository<Worker>, IWorkerAsyncRepository, IAsyncRepository<Worker>
    {
        public WorkerAsyncRepository(IWhenItsDoneDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IEnumerable<WorkerWithDishesDTO>> GetWorkersWithDishes()
        {
            return Task.Run<IEnumerable<WorkerWithDishesDTO>>(() =>
            {
                return base.DbSet.ProjectToList<WorkerWithDishesDTO>();
            });
        }
    }
}
