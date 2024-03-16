using Notification.API.Entities;

namespace Notification.API.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAll();
        Task<IEnumerable<Message>> GetMessagesByUser(string userId);

        Task CreateMessage(Message message);
        Task<bool> UpdateMessage(Message message);
        Task<bool> DeteleMessage(string id);
    }
}
