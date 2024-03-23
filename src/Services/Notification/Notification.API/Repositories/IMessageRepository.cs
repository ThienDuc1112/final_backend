using Notification.API.DTOs;
using Notification.API.Entities;

namespace Notification.API.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAll();
        Task<MessageResult> GetMessagesByUser(string userId, int page);

        Task CreateMessage(Message message);
        Task<bool> UpdateMessage(Message message);
        Task<bool> DeteleMessage(string id);
        Task<bool> IsExisted(string userId, string type, int ApplicationId);
        Task<long> GetNewMessageCount(string userId);
    }
}
