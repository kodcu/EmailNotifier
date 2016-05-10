using Notifier.Message;
using Notifier.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifier.Extention;

namespace Notifier
{
    public class NotificationController
    {
        public NotificationController() { }

        public void Check()
        {
            NotificationRepository repository = new NotificationRepository();
            IEnumerable<MailNotificationMessage> list = repository.GetDocuments() as IEnumerable<MailNotificationMessage>;

            bool controldummy;
            foreach (MailNotificationMessage item in list)
            {
                try
                {
                    item.SendMail();
                    controldummy = true;
                }
                catch (Exception)
                {
                    controldummy = false;
                    item.Status = EmailStatus.ERROR;                    
                }

                if (controldummy)
                    item.Status = EmailStatus.SEND;

                item.TryCount++;
                repository.UpdateDocument(item);
            }
        }
    }
}
