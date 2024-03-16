using EventBus.Messages.Events;
using MassTransit;
using Notification.API.DTOs;
using Notification.API.Entities;
using Notification.API.Repositories;

namespace Notification.API.EventBusConsumer
{
    public class SendingMessageConsumer : IConsumer<SendingMessageEvent>
    {
        private readonly IMessageRepository _messageRepository;
        public SendingMessageConsumer(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Consume(ConsumeContext<SendingMessageEvent> context)
        {
            var response = context.Message;
            var message = new Message
            {
                BusinessName = response.BusinessName,
                FullName = response.FullName,
                Title = response.Title,
                Type = response.Type,
                UserId = response.UserId,
                CreatedDate = response.CreatedDate,
                IsDelete = false,
                IsSeen = false,
            };

            await _messageRepository.CreateMessage(message);
        }
    }
}
