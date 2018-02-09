using EstudoTask.Operacoes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EstudoTask
{
    public class Executor
    {
        private readonly ExecutandoOperacaoBasica _operacoesBasicas = new ExecutandoOperacaoBasica();
        private readonly ExecutandoOperacaoAssincronaComOutraAssincronaDentro _asyncDentroDeAsync
            = new ExecutandoOperacaoAssincronaComOutraAssincronaDentro();
        private readonly EnviandoVariosEmails _envioEmails = new EnviandoVariosEmails();
        private readonly ConsultandoPrimeiroServicoQueResponder _consultandoServicos = new ConsultandoPrimeiroServicoQueResponder();
        private readonly LoopsParalelos _loopsParalelos = new LoopsParalelos();

        public async Task CriarArquivoTextoComAwaitSemAwait()
        {
            var resultado = await _operacoesBasicas.ExecutarAsync();
            Console.WriteLine($"{resultado} \n\n");
        }

        public void TestandoCancelamento()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var task = _operacoesBasicas.CancelandoTask(token);

            Console.WriteLine("Para cancelar aperte 1");
            var teclaApertada = Console.ReadLine();
            if (teclaApertada == "1")
            {
                if (task.IsCompleted || task.IsFaulted)
                {
                    Console.WriteLine("Não é possível cancelar pois a thread já concluiu a tarefa. \n\n");
                    return;
                }
                tokenSource.Cancel();
                Console.WriteLine("Operação de cancelamento concluída \n\n");
            }
        }

        public async Task ConsultandoOperacaoAssincronaComOutraDentro()
        {
            var textoComposto = await _asyncDentroDeAsync.ExecutarAsync();
            Console.WriteLine($"Operação concluída texto final: {textoComposto} \n\n");
        }

        public async Task EnviandoEmails()
        {
            var dataComecoEnvio = DateTime.Now;
            Console.WriteLine("Comecei a enviar e-mail");
            await _envioEmails.Enviar();
            var dataTerminoEmails = DateTime.Now;
            Console.WriteLine($"Conclui o envio dos e-mails tempo demorado: {(dataTerminoEmails - dataComecoEnvio).TotalSeconds} \n\n");
        }

        public async Task EnviandoEmailsComExcecaoNoMeio()
        {
            var dataComecoEnvio = DateTime.Now;
            await _envioEmails.EnviandoComExcecaoNoMeio();
            var dataTerminoEmails = DateTime.Now;
            Console.WriteLine($"Conclui o envio dos e-mails tempo demorado: {(dataTerminoEmails - dataComecoEnvio).TotalSeconds} \n\n");
        }

        public async Task ConsultandoServicosEFicandoComPrimeiro()
        {
            var dataComecoEnvio = DateTime.Now;
            var servicoQueRespondeu = await _consultandoServicos.Consultar();
            var dataTerminoEmails = DateTime.Now;
            Console.WriteLine($"Conclui a consulta dos serviços tempo demorado: {(dataTerminoEmails - dataComecoEnvio).TotalSeconds}\n Serviço que respondeu: {servicoQueRespondeu} \n\n");
        }

        public async Task ConsultandoServicosTratandoQuemResponde()
        {
            var dataInicio = DateTime.Now;
            await _consultandoServicos.ConsultandoServicosETratandoConformeRespondem();
            var dataFim = DateTime.Now;
            Console.WriteLine($"\nOperação concluída tempo total: {(dataFim - dataInicio).TotalSeconds} \n\n");
        }

        public async Task ConsultandoServicosComExcecaoNoMeio()
        {
            var dataInicio = DateTime.Now;
            var servico = await _consultandoServicos.ConsultarServicoComException();
            var dataFim = DateTime.Now;
            Console.WriteLine($"\nOperação concluída tempo total: {(dataFim - dataInicio).TotalSeconds}\n Serviço respondido: {servico} \n\n");
        }

        public void LoopsParalelos()
        {
            _loopsParalelos.ProcessarLista();
        }
    }
}