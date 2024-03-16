using MongoDB.Driver;
using Notification.API.Entities;

namespace Notification.API.Data
{
    public class NotificationContext : INotificationContext
    {
        public NotificationContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Messages = database.GetCollection<Message>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<Message> Messages { get; }
    }
}
