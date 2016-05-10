using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Message
{
    
    public class NotificationMessage
        : BaseMessage
    {
        public int TryCount { get; set; }
        
        public string MachineName { get; set; }
        public string ProcessName { get; set; }
    }

}
