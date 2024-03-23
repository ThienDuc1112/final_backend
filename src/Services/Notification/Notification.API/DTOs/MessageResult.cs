using Notification.API.Entities;

namespace Notification.API.DTOs
{
    public class MessageResult
    {
        public List<Message> Messages { get; set; }
        public long TotalMessage{ get; set; }
    }
}
