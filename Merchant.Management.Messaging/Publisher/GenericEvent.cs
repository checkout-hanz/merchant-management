using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Management.Messaging.Publisher
{
    internal class GenericEvent : IEvent
    {
        public string EventName { get; set; }
    }
}
