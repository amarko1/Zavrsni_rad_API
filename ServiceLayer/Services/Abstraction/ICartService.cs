using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(int userId);
        Task AddToCartAsync(AddToCartDTO addToCartDTO);
        Task UpdateCartItemAsync(UpdateCartItemDTO updateCartItemDTO);
        Task RemoveFromCartAsync(RemoveFromCartDTO removeFromCartDTO);
        Task ClearCartAsync(int userId);
    }

}
