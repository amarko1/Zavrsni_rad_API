using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Dto;

namespace Zavrsni_rad_API.Hubs
{
    public class OrderHub : Hub
    {
        public async Task NotifyNewOrder(OrderDTO order)
        {
            await Clients.All.SendAsync("ReceiveOrder", order);
        }
    }
}
