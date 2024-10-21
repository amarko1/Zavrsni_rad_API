using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dto
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? EstimatedPickupDate { get; set; }
        public string? OperatorNote { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }

    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string CakeName { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string? Customizations { get; set; }
    }

    public class OrderCreateDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<OrderItemCreateDTO> OrderItems { get; set; } = new List<OrderItemCreateDTO>();
    }

    public class OrderItemCreateDTO
    {
        public int CakeId { get; set; }
        public int Quantity { get; set; }
        public string? Customizations { get; set; }
    }

    public class OrderStatusUpdateDTO
    {
        public int Id { get; set; }
        public OrderStatus NewStatus { get; set; }
    }

    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? OperatorNote { get; set; }
        public DateTime? EstimatedPickupDate { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItemUpdateDTO> OrderItems { get; set; } = new List<OrderItemUpdateDTO>();
    }
    public class OrderItemUpdateDTO
    {
        public int Id { get; set; }
        public int CakeId { get; set; }
        public int Quantity { get; set; }
        public string? Customizations { get; set; }
    }

    public class OrderApproveDTO
    {
        public int Id { get; set; }
        public DateTime EstimatedPickupDate { get; set; }
        public string? OperatorNote { get; set; }
    }

    public class OrderRejectDTO
    {
        public int Id { get; set; }

        public string OperatorNote { get; set; } = string.Empty;
    }
}
