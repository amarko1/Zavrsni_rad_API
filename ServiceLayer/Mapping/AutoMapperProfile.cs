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
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Cake, CakeDto>().ReverseMap();
            CreateMap<Cake, CakeDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Recipe, RecipeDto>().ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

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

            CreateMap<RecipeCreateRequest, Recipe>()
                .ForMember(dest => dest.RecipeIngredients, opt => opt.MapFrom(src => src.RecipeIngredients))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<RecipeIngredientRequest, RecipeIngredient>().ReverseMap();

            CreateMap<TaskItem, TaskItemDto>().ReverseMap();

            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>();
            CreateMap<Order, OrderDTO>();

            CreateMap<OrderItemCreateDTO, OrderItem>();
            CreateMap<OrderItemUpdateDTO, OrderItem>();
            CreateMap<OrderItem, OrderItemDTO>();

            CreateMap<Cart, CartDTO>();
            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.CakeName, opt => opt.MapFrom(src => src.Cake.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Cake.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Cake.Price * src.Quantity));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

        }
    }
}
