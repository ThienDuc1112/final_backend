using Microsoft.AspNetCore.SignalR;
using Notification.API.Entities;
using Notification.API.Repositories;

namespace Notification.API.MyHub
{
    public class MessageHub : Hub
    {
        public async Task SendNewMessageCount(string userId, long count, Message message)
        {

            await Clients.All.SendAsync("ReceiveNotificationCount", userId, count, message);
        }
    }
}
