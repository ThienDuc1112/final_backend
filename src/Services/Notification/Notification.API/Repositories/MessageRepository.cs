using MongoDB.Driver;
using Notification.API.Data;
using Notification.API.DTOs;
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
            return await _notificationContext.Messages.Find(m => true).SortByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<MessageResult> GetMessagesByUser(string userId, int page)
        {
            int pageSize = 8;
            int skip = (page - 1) * pageSize;

            var query = _notificationContext.Messages.Find(m => m.UserId == userId);

            long totalRows = await query.CountDocumentsAsync();

            var messages = await query
                .SortByDescending(m => m.CreatedDate)
                .Skip(skip)
                .Limit(pageSize)
                .ToListAsync();
            MessageResult result = new MessageResult
            {
                Messages = messages,
                TotalMessage = totalRows
            };

            return result;
        }

        [Obsolete]
        public async Task<long> GetNewMessageCount(string userId)
        {
            FilterDefinition<Message> filter = Builders<Message>.Filter.And(
                Builders<Message>.Filter.Eq(m => m.UserId, userId),
                Builders<Message>.Filter.Eq(m => m.IsSeen, false)
            );

            return await _notificationContext.Messages.CountAsync(filter);
        }

        public async Task<bool> IsExisted(string userId, string type, int ApplicationId)
        {
            FilterDefinition<Message> filter = Builders<Message>.Filter.And(
                Builders<Message>.Filter.Eq(m => m.UserId, userId),
                 Builders<Message>.Filter.Eq(m => m.Type, type),
                  Builders<Message>.Filter.Eq(m => m.ApplicationId, ApplicationId)
                );

            return await _notificationContext.Messages.Find(filter).AnyAsync();
        }

        public async Task<bool> UpdateMessage(Message message)
        {
            var updateResult = await _notificationContext.Messages
                .ReplaceOneAsync(filter: p => p.Id == message.Id, replacement: message);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
