using AutoMapper;
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
            // Već postojeće mape
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Cake, CakeDto>().ReverseMap();
            CreateMap<Cake, CakeDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // Mapiranje za Recipe <-> RecipeDto
            CreateMap<Recipe, RecipeDto>().ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // **Mapiranje za RecipeIngredient i RecipeIngredientDto**
            CreateMap<RecipeIngredient, RecipeIngredientDto>().ReverseMap();

            // Mapiranje za Ingredient <-> IngredientDto
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            // Mapiranje za Supply <-> SupplyDto
            CreateMap<Supply, SupplyDto>().ReverseMap();
            CreateMap<Supply, SupplyDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // Mapiranje za SupplyCreateRequest <-> Supply
            CreateMap<SupplyCreateRequest, Supply>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            // Mapiranje za CakeUpdateRequest <-> Cake
            CreateMap<CakeUpdateRequest, Cake>()
                .ReverseMap()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            // **Nova mapa za RecipeCreateRequest <-> Recipe**
            CreateMap<RecipeCreateRequest, Recipe>()
                .ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.RecipeIngredients))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            // **Provjeri mapiranje za RecipeIngredientDto**
            CreateMap<RecipeIngredientRequest, RecipeIngredient>().ReverseMap();
        }
    }
}
