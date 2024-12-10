using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Dto;

namespace Zavrsni_rad_API.Hubs
{
    public class SignalRService
    {
        private IHubContext<OrderHub> _hubContext;

        public SignalRService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewOrder(OrderDTO orderDetails)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveOrder", orderDetails);
        }
    }
}
