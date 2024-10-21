using DAL.Models;
using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(OrderCreateDTO orderCreateDTO);
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDTO>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId);
        Task UpdateOrderAsync(OrderUpdateDTO orderUpdateDTO);
        Task ApproveOrderAsync(OrderApproveDTO approveDTO);
        Task RejectOrderAsync(OrderRejectDTO rejectDTO);
        Task UpdateOrderStatusAsync(OrderStatusUpdateDTO statusUpdateDTO);
    }

}
