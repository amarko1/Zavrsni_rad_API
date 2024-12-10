using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using Zavrsni_rad_API.Hubs;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly SignalRService _signalRService;

        public OrderController(IOrderService orderService, SignalRService signalRService)
        {
            _orderService = orderService;
            _signalRService = signalRService;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderStatus? status = null, [FromQuery] DateTime? dateFrom = null, [FromQuery] DateTime? dateTo = null)
        {
            var orders = await _orderService.GetOrdersAsync(status, dateFrom, dateTo);
            return Ok(orders);
        }


        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("GetOrdersByStatus/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatus status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        [HttpGet("GetOrdersByUserId/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderId = await _orderService.CreateOrderAsync(orderCreateDTO);

            var orderDetails = await _orderService.GetOrderByIdAsync(orderId);
            if (orderDetails == null) 
            {
                return BadRequest(ModelState); 
            }
            await _signalRService.NotifyNewOrder(orderDetails);

            return Ok(new { OrderId = orderId });
        }

        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDTO orderUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _orderService.UpdateOrderAsync(orderUpdateDTO);
            return Ok();
        }

        [HttpPost("ApproveOrder")]
        public async Task<IActionResult> ApproveOrder([FromBody] OrderApproveDTO approveDTO)
        {
            await _orderService.ApproveOrderAsync(approveDTO);
            return Ok();
        }

        [HttpPost("RejectOrder")]
        public async Task<IActionResult> RejectOrder([FromBody] OrderRejectDTO rejectDTO)
        {
            await _orderService.RejectOrderAsync(rejectDTO);
            return Ok();
        }

        [HttpPost("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateDTO statusUpdateDTO)
        {
            await _orderService.UpdateOrderStatusAsync(statusUpdateDTO);
            return Ok();
        }
    }
}
