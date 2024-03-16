using Microsoft.AspNetCore.Mvc;

namespace Notification.API.DTOs
{
    public class CreateMessage 
    {
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
