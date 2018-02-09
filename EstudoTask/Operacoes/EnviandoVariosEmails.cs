using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstudoTask.Operacoes
{
    public class EnviandoVariosEmails
    {
        private readonly EmailSender _emailSender = new EmailSender();

        public async Task Enviar()
        {
            var emailsEnviar = new List<EmailMessage>();
            var emailTo = new List<string>() {"drprado2@gmail.com"};

            for(var i = 0; i < 5; i++)
            {
                emailsEnviar.Add(new EmailMessage(emailTo, "Eita async é bom demais", $"Esse é o conteúdo {i}"));
            }

            await _emailSender.SendManyAsync(emailsEnviar);
        }

        public async Task EnviandoComExcecaoNoMeio()
        {
            var emailsEnviar = new List<EmailMessage>();
            var emailTo = new List<string>() {"drprado2@gmail.com"};

            for(var i = 0; i < 3; i++)
            {
                if(i == 1)
                    emailsEnviar.Add(null);
                else
                emailsEnviar.Add(new EmailMessage(emailTo, "Eita async é bom demais", $"Esse é o conteúdo {i}"));
            }

            await _emailSender.SendManyAsync(emailsEnviar);
        }

        private async Task<bool> EnviarEmail(Email email)
        {
            // enviando email
            return await Task.Run(() => 
            {
                var conteudo = email.Conteudo;
                Thread.Sleep(5000);
                return true;
            });
        }
    }

    public class Email
    {
        public Email(string endereco, string conteudo)
        {
            Endereco = endereco;
            Conteudo = conteudo;
        }

        public string Endereco { get; set; }
        public string Conteudo { get; set; }
    }
}
