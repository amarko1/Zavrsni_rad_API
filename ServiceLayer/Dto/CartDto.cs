using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class CartDTO
    {
        public int Id { get; set; }
        public List<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CartItemDTO
    {
        public int Id { get; set; }
        public int CakeId { get; set; }
        public string CakeName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Customizations { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class AddToCartDTO
    {
        public int UserId { get; set; }
        public int CakeId { get; set; }
        public int Quantity { get; set; }
        public string Customizations { get; set; } = string.Empty;
    }

    public class UpdateCartItemDTO
    {
        public int UserId { get; set; }
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public string Customizations { get; set; } = string.Empty;
    }

    public class RemoveFromCartDTO
    {
        public int UserId { get; set; }

        public int CartItemId { get; set; }
    }
}
