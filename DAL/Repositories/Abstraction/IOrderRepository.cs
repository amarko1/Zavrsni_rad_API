using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstraction
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order newOrder);
        Task<Order?> GetOrderAsync(int id);
        Task<IEnumerable<Order>> GetOrdersAsync(OrderStatus? status, DateTime? dateFrom, DateTime? dateTo);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    }
}
