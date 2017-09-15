using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstudoTask.Operacoes
{
    public class ConsultandoPrimeiroServicoQueResponder
    {
        public async Task Consultar()
        {
            var listaThreadsConsultarServicos = new List<Task<string>>();
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico1.com.br", 10000));
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico2.com.br", 8000));
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico3.com.br", 3000));

            var threadRetornada = await Task.WhenAny(listaThreadsConsultarServicos);
            var resultado = await threadRetornada;
            var aa = "";
        }

        public async Task ConsultarServicoComException()
        {
            var listaThreadsConsultarServicos = new List<Task<string>>();
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico1.com.br", 10000));
            listaThreadsConsultarServicos.Add(ConsultarServicoExternoComException("www.servico2.com.br", 3000));
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico3.com.br", 8000));

            string resultado = "";
            do
            {
                try
                {
                    var threadRetornada = await Task.WhenAny(listaThreadsConsultarServicos);
                    resultado = await threadRetornada;
                }
                catch (Exception erro)
                {
                    var threadQueDeuErro = listaThreadsConsultarServicos.FirstOrDefault(x => x.IsFaulted);
                    listaThreadsConsultarServicos.Remove(threadQueDeuErro);
                }
            }
            while (string.IsNullOrWhiteSpace(resultado));
            var aa = "";
        }

        public async Task ConsultandoServicosETratandoConformeRespondem()
        {
            var listaThreadsConsultarServicos = new List<Task<string>>();
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico1.com.br", 30000));
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico2.com.br", 12000));
            listaThreadsConsultarServicos.Add(ConsultarServicoExterno("www.servico3.com.br", 3000));

            do
            {
                var threadResultado = await Task.WhenAny(listaThreadsConsultarServicos);
                var resultado = await threadResultado;

                // faco algo com o resultado

                listaThreadsConsultarServicos.Remove(threadResultado);
            }
            while (listaThreadsConsultarServicos.Count > 0);
        }

        private async Task<string> ConsultarServicoExterno(string enderecoDoServico, int tempoDeRetorno)
        {
            return await Task.Run(() => { Thread.Sleep(tempoDeRetorno); return $"Resposta do serviço. {enderecoDoServico}"; });
        }

        private async Task<string> ConsultarServicoExternoComException(string enderecoDoServico, int tempoDeRetorno)
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(tempoDeRetorno);
                throw new Exception("Deu erro");
                return $"Resposta do serviço. {enderecoDoServico}";
            });
        }


    }
}
