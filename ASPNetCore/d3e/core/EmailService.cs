using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Classes;
using models;
using store;

namespace d3e.core
{
    public class EmailService
    {

        private ConcurrentQueue<EmailMessage> Emails = new ConcurrentQueue<EmailMessage>();

        private TransactionWrapper Wrapper;

        public void Init()
        {
            //Task.Run(() => Run());

        }

        public void Send(EmailMessage mail)
        {
            if (mail == null)
            {
                return;
            }
            PushMail(mail);
        }

        public void SendVertificationEmail(string email, string context, string body, bool html, string subject)
        {
            if (email == null || context == null)
            {
                return;
            }

            string token = Guid.NewGuid().ToString();

            VerificationData data = new VerificationData();
            data.Method = email;
            data.Context = context;
            data.Token = token;
            data.Body = body;
            data.Subject = subject;
            try
            {
                Wrapper.DoInTransaction(() =>
                {
                    Database.Get().Save(data);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(token, ex);
            }

            string link = Env.Get().BaseHttpUrl + "/verify?code=" + token;

            if (String.IsNullOrEmpty(body))
            {
                body = "This is your vertification link : ${link}";
            }
            body = body.Replace("{link}", link);

            if (String.IsNullOrEmpty(subject))
            {
                subject = "Vertification link";
            }
            EmailMessage msg = new EmailMessage();
            msg.To = [email];
            msg.Subject = subject;
            msg.Body = body;
            msg.Html = html;
            Send(msg);
        }

        private void PushMail(EmailMessage mail)
        {
            Emails.Enqueue(mail);
        }

        private string GetEnvString(string str)
        {
            return EnvironmentHelper.GetEnvString(str);
        }

        private void SendEmail(EmailMessage email)
        {
            var smtpClient = new SmtpClient
            {
                Host = GetEnvString("{env.mail.smtp.host}"),
                Port = int.Parse(GetEnvString("{env.mail.smtp.port}")),
                EnableSsl = true,
                Credentials = new NetworkCredential(GetEnvString("{env.mail.uname}"), GetEnvString("{env.mail.pwd}"))
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(GetEnvString("{env.mail.sender}")),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = email.Html
            };

            foreach (var recipient in email.To)
            {
                mailMessage.To.Add(recipient);
            }

            foreach (var cc in email.Cc)
            {
                mailMessage.CC.Add(cc);
            }

            foreach (var bcc in email.Bcc)
            {
                mailMessage.Bcc.Add(bcc);
            }

            smtpClient.Send(mailMessage);
        }
    }
}
