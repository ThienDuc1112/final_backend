using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Notification.API.DTOs;
using Notification.API.Entities;
using Notification.API.MyHub;
using Notification.API.Repositories;

namespace Notification.API.EventBusConsumer
{
    public class SendingMessageConsumer : IConsumer<SendingMessageEvent>
    {
        private readonly IMessageRepository _messageRepository;
        private IHubContext<MessageHub> _messageHub;
        public SendingMessageConsumer(IMessageRepository messageRepository, IHubContext<MessageHub> messagenHubContext)
        {
            _messageRepository = messageRepository;
            _messageHub = messagenHubContext;
        }

        public async Task Consume(ConsumeContext<SendingMessageEvent> context)
        {
            var response = context.Message;
            var message = new Message
            {
                ApplicationId = response.ApplicationId,
                BusinessName = response.BusinessName,
                FullName = response.FullName,
                Title = response.Title,
                Type = response.Type,
                UserId = response.UserId,
                CreatedDate = response.CreatedDate,
                IsDelete = false,
                IsSeen = false,
                
            };
            if(!await _messageRepository.IsExisted(message.UserId, message.Type, message.ApplicationId))
            {
                await _messageRepository.CreateMessage(message);
                long count = await _messageRepository.GetNewMessageCount(message.UserId);
                await _messageHub.Clients.All.SendAsync("ReceiveNotificationCount", message.UserId, count, message);
            }
           
        }
    }
}
