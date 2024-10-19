using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? EstimatedPickupDate { get; set; }
        public string? OperatorNote { get; set; }
        public decimal TotalPrice { get; set; }
        public int? UserId { get; set; } 
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        New,
        PendingApproval,
        Approved,
        Rejected,
        InPreparation,
        ReadyForPickup,
        Completed
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string CakeName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Customizations { get; set; }
        public int OrderId { get; set; }
        public int CakeId { get; set; }
        public Order order { get; set; }
    }
}
