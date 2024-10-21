﻿using AutoMapper;
using DAL.Models;
using DAL.Repositories.Abstraction;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICakeRepository _cakeRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, ICakeRepository cakeRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cakeRepository = cakeRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            var newOrder = _mapper.Map<Order>(orderCreateDTO);
            newOrder.CreatedAt = DateTime.UtcNow;
            newOrder.UpdatedAt = DateTime.UtcNow;
            newOrder.OrderStatus = OrderStatus.New;

            var orderItems = await CreateOrderItemsAsync(orderCreateDTO.OrderItems);
            newOrder.OrderItems = orderItems;
            newOrder.TotalPrice = orderItems.Sum(item => item.TotalPrice);

            await _orderRepository.CreateOrderAsync(newOrder);
            return newOrder.Id;
        }

        private async Task<List<OrderItem>> CreateOrderItemsAsync(IEnumerable<OrderItemCreateDTO> orderItemDTOs)
        {
            var orderItems = new List<OrderItem>();

            foreach (var itemDto in orderItemDTOs)
            {
                var cake = await _cakeRepository.GetCakeAsync(itemDto.CakeId);
                if (cake == null)
                {
                    throw new InvalidOperationException($"Cake with ID {itemDto.CakeId} not found.");
                }

                var orderItem = new OrderItem
                {
                    CakeId = cake.Id,
                    CakeName = cake.Name,
                    Quantity = itemDto.Quantity,
                    UnitPrice = cake.Price,
                    TotalPrice = cake.Price * itemDto.Quantity,
                    Customizations = itemDto.Customizations
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }

        private async Task<List<OrderItem>> CreateOrderItemsAsync(IEnumerable<OrderItemUpdateDTO> orderItemDTOs)
        {
            var orderItems = new List<OrderItem>();

            foreach (var itemDto in orderItemDTOs)
            {
                var cake = await _cakeRepository.GetCakeAsync(itemDto.CakeId);
                if (cake == null)
                {
                    throw new InvalidOperationException($"Cake with ID {itemDto.CakeId} not found.");
                }

                var orderItem = new OrderItem
                {
                    CakeId = cake.Id,
                    CakeName = cake.Name,
                    Quantity = itemDto.Quantity,
                    UnitPrice = cake.Price,
                    TotalPrice = cake.Price * itemDto.Quantity,
                    Customizations = itemDto.Customizations
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }


        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task UpdateOrderAsync(OrderUpdateDTO orderUpdateDTO)
        {
            var existingOrder = await _orderRepository.GetOrderAsync(orderUpdateDTO.Id);
            if (existingOrder == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            if (orderUpdateDTO.OrderItems != null && orderUpdateDTO.OrderItems.Any())
            {
                var orderItems = await CreateOrderItemsAsync(orderUpdateDTO.OrderItems);
                existingOrder.OrderItems = orderItems;
                existingOrder.TotalPrice = orderItems.Sum(item => item.TotalPrice);
            }

            existingOrder.OperatorNote = orderUpdateDTO.OperatorNote;
            existingOrder.EstimatedPickupDate = orderUpdateDTO.EstimatedPickupDate;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateOrderAsync(existingOrder);
        }

        public async Task ApproveOrderAsync(OrderApproveDTO approveDTO)
        {
            var order = await _orderRepository.GetOrderAsync(approveDTO.Id);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            order.EstimatedPickupDate = approveDTO.EstimatedPickupDate;
            order.OperatorNote = approveDTO.OperatorNote;
            order.OrderStatus = OrderStatus.Approved;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task RejectOrderAsync(OrderRejectDTO rejectDTO)
        {
            var order = await _orderRepository.GetOrderAsync(rejectDTO.Id);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            order.OperatorNote = rejectDTO.OperatorNote;
            order.OrderStatus = OrderStatus.Rejected;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task UpdateOrderStatusAsync(OrderStatusUpdateDTO statusUpdateDTO)
        {
            var order = await _orderRepository.GetOrderAsync(statusUpdateDTO.Id);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            order.OrderStatus = statusUpdateDTO.NewStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
