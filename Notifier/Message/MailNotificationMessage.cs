using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Message
{
    public class MailNotificationMessage
        : NotificationMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
        public EmailStatus Status { get; set; }
        public bool IsHtml { get; set; }
    }
}
