using AutoMapper;
using DAL.AppDbContext;
using DAL.Models;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICakeRepository _cakeRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public CartService(ApplicationContext context, ICartRepository cartRepository, ICakeRepository cakeRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _cakeRepository = cakeRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<CartDTO> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _cartRepository.CreateCartAsync(cart);
            }
            var cartDTO = _mapper.Map<CartDTO>(cart);
            return cartDTO;
        }

        public async Task AddToCartAsync(AddToCartDTO addToCartDTO)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(addToCartDTO.UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = addToCartDTO.UserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _cartRepository.CreateCartAsync(cart);
            }

            var cake = await _cakeRepository.GetCakeAsync(addToCartDTO.CakeId);
            if (cake == null)
            {
                throw new InvalidOperationException("Cake not found.");
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.CakeId == addToCartDTO.CakeId && ci.Customizations == addToCartDTO.Customizations);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += addToCartDTO.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CakeId = cake.Id,
                    Quantity = addToCartDTO.Quantity,
                    Customizations = addToCartDTO.Customizations,
                    CartId = cart.Id,
                    Cake = cake,
                    Cart = cart
                };
                cart.CartItems.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task UpdateCartItemAsync(UpdateCartItemDTO updateCartItemDTO)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(updateCartItemDTO.UserId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == updateCartItemDTO.CartItemId);
            if (cartItem == null)
            {
                throw new InvalidOperationException("Cart item not found.");
            }

            cartItem.Quantity = updateCartItemDTO.Quantity;
            cartItem.Customizations = updateCartItemDTO.Customizations;
            cart.UpdatedAt = DateTime.UtcNow;

            await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task RemoveFromCartAsync(RemoveFromCartDTO removeFromCartDTO)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(removeFromCartDTO.UserId);
            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == removeFromCartDTO.CartItemId);
            if (cartItem == null)
            {
                throw new InvalidOperationException("Cart item not found.");
            }

            cart.CartItems.Remove(cartItem);
            cart.UpdatedAt = DateTime.UtcNow;

            await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null || !cart.CartItems.Any())
            {
                return;
            }

            // Eksplicitno brisanje stavki
            _context.CartItems.RemoveRange(cart.CartItems);

            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }

}
