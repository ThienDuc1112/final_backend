using MongoDB.Driver;
using Notification.API.Data;
using Notification.API.Entities;

namespace Notification.API.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly INotificationContext _notificationContext;
        public MessageRepository(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }   

        public async Task CreateMessage(Message message)
        {
            await _notificationContext.Messages.InsertOneAsync(message);
        }

        public async Task<bool> DeteleMessage(string id)
        {
            FilterDefinition<Message> filter = Builders<Message>.Filter.Eq(m => m.Id, id);
            DeleteResult result = await _notificationContext.Messages.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _notificationContext.Messages.Find(m => true).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByUser(string userId)
        {
            return await _notificationContext.Messages.Find(m => m.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateMessage(Message message)
        {
            var updateResult = await _notificationContext.Messages
                .ReplaceOneAsync(filter: p => p.Id == message.Id, replacement: message);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
