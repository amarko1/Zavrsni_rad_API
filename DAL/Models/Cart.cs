using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int CakeId { get; set; }
        public int Quantity { get; set; }
        public Cake Cake { get; set; }
        public Cart Cart { get; set; }
        public string Customizations { get; set; }
    }
}
