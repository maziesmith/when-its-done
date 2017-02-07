﻿using System.Linq;

using AutoMapper;

using WhenItsDone.DTOs.DishViewsDTOs;
using WhenItsDone.Models;

namespace WhenItsDone.WebFormsClient.App_Start.AutomapperProfiles
{
    public class DishViewsProfile : Profile
    {
        public DishViewsProfile()
        {
            this.CreateMap<Dish, DishWithPhotosDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Photos, opts => opts.MapFrom(x => x.PhotoItems));

            this.CreateMap<Dish, DishBasicsInfoDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Recipe.Name))
                .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rating))
                .ForMember(dest => dest.RecipeId, opts => opts.MapFrom(src => src.RecipeId));

            this.CreateMap<Dish, NamePhotoDishViewDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Recipe.Name))
                .ForMember(dest => dest.PhotoItemUrl, opts => opts.MapFrom(src => src.PhotoItems.FirstOrDefault().Url));

            this.CreateMap<Dish, NamePhotoRatingDishViewDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Recipe.Name))
                .ForMember(dest => dest.PhotoItemUrl, opts => opts.MapFrom(src => src.PhotoItems.FirstOrDefault().Url))
                .ForMember(dest => dest.Rating, opts => opts.MapFrom(src => src.Rating));

            this.CreateMap<Dish, DishWithIngradientsDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.RecipeId, opts => opts.MapFrom(x => x.RecipeId))
                .ForMember(dest => dest.Ingradients, opts => opts.MapFrom(x => x.Recipe.Ingredients));
        }
    }
}