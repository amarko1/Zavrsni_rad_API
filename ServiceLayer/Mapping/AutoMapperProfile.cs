﻿using AutoMapper;
using DAL.Models;
using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Cake, CakeDto>().ReverseMap();
            CreateMap<Cake, CakeDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<RecipeIngredient, RecipeIngredientDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Supply, SupplyDto>().ReverseMap();
            CreateMap<Supply, SupplyDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<SupplyCreateRequest, Supply>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
            CreateMap<CakeUpdateRequest, Cake>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));
        }
    }
}
