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
        public async Task Enviar()
        {
            var emailsEnviar = new List<Email>();
            for(var i = 0; i < 10; i++)
            {
                emailsEnviar.Add(new Email($"Pedro{i}@gmail.com", "Esse é o conteúdo {i}"));
            }

            var threadsEnviarEmails = emailsEnviar.Select(x => EnviarEmail(x)).ToList();

            await Task.WhenAll(threadsEnviarEmails);

            var aa = "aguardando";
        }

        public async Task EnviandoComExcecaoNoMeio()
        {
            var emailsEnviar = new List<Email>();
            for(var i = 0; i < 3; i++)
            {
                if(i == 1)
                    emailsEnviar.Add(null);
                else
                    emailsEnviar.Add(new Email($"Pedro{i}@gmail.com", "Esse é o conteúdo {i}"));

            }

            var threadsEnviarEmails = emailsEnviar.Select(x => EnviarEmail(x)).ToList();

            try
            {
                await Task.WhenAll(threadsEnviarEmails);
            }
            catch(Exception e)
            {
                var erro = e;

                // se quiser pegar todos os erros
                var threadsQueDeramErro = threadsEnviarEmails.Where(x => x.IsFaulted).ToList();
                var erroDeExemplo = threadsQueDeramErro[0].Exception;
            }

            var aa = "aguardando";
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
