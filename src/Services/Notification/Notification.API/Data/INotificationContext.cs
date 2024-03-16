using MongoDB.Driver;
using Notification.API.Entities;

namespace Notification.API.Data
{
    public interface INotificationContext
    {
        IMongoCollection<Message> Messages { get; }
    }
}
