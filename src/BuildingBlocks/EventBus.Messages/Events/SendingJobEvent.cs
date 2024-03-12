using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class SendingJobEvent : IntegrationBaseEvent
    {
        public string Title { get; set; }
    }
}
