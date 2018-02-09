using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EstudoTask
{
    public class EmailSender 
    {
        private const string SmtpClient = "smtp.gmail.com";
        private const string UserCredentials = "estudo.async.hb@gmail.com";
        private const string PasswordCredentials = "Hbasync2018";


        public void SendAsync(EmailMessage email)
        {
            Task.Run(() => Send(email));
        }

        private void Send(EmailMessage email)
        {
            using (var smtp = new SmtpClient(SmtpClient))
            {
                var mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(UserCredentials);

                foreach (var userTo in email.UsersTo)
                {
                    mailMessage.To.Add(userTo);
                }

                mailMessage.Subject = email.Subject;

                mailMessage.Body = email.Body;

                mailMessage.BodyEncoding = Encoding.UTF8;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;

                smtp.Credentials = new NetworkCredential(UserCredentials, PasswordCredentials);

                smtp.Send(mailMessage);
            }
        }

        public async Task SendManyAsync(IList<EmailMessage> emails)
        {
            var tasksSend = new List<Task>();

            foreach (var email in emails)
            {
                tasksSend.Add(Task.Run(() => Send(email)));
            }

            try
            {
                await Task.WhenAll(tasksSend);
            }
            catch
            {
                var emailsQueDeramErrado = tasksSend.Where(x => x.IsFaulted).ToList();

                emailsQueDeramErrado.ForEach(x => Console.WriteLine($"E-mail que deu errado erro: {x.Exception}"));
            }
        }
    }
}