using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class SendingMessageEvent : IntegrationBaseEvent
    {
        public string UserId { get; set; }
        public string BusinessName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
