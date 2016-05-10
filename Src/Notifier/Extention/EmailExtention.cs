using Notifier.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notifier.Extention
{
    public static class EmailExtention
    {
        public static bool SendMail(this MailNotificationMessage msg)
        {
            bool rv = true;

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(msg.From);
            mailMessage.To.Add(new MailAddress(msg.To));
            mailMessage.Bcc.Add(new MailAddress(msg.Bcc));
            mailMessage.CC.Add(new MailAddress(msg.Cc));

            mailMessage.Subject = msg.Subject;
            mailMessage.Body = msg.Body;
            mailMessage.IsBodyHtml = msg.IsHtml;
            mailMessage.Priority = MailPriority.Normal;

            // normally this configuration settings gets from config files or config tables
            SmtpClient mSmtpClient = new SmtpClient();
            mSmtpClient.Host = EnvironmentConstants.SMTP_HOST;
            mSmtpClient.Port = EnvironmentConstants.SMTP_PORT;
            mSmtpClient.EnableSsl = true;

            try
            {
                mSmtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                rv = false;
            }
            return rv;   
        }
    }
}
