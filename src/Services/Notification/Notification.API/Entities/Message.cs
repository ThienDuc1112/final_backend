using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notification.API.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BusinessName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSeen { get; set; }
        public bool IsDelete { get; set; }

    }
}
