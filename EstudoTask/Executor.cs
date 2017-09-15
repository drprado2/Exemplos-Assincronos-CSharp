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


        public async Task CriarArquivoTextoComAwaitSemAwait()
        {
            var resultado = await _operacoesBasicas.ExecutarAsync();
            Console.WriteLine($"{resultado} \n\n");
        }

        public void TestandoCancelamento()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            _operacoesBasicas.CancelandoTask(token);

            Console.WriteLine("Para cancelar aperte 1");
            var teclaApertada = Console.ReadLine();
            if (teclaApertada == "1")
                tokenSource.Cancel();
            Console.WriteLine("Operação de cancelamento concluída \n\n");
        }

        public async Task ConsultandoOperacaoAssincronaComOutraDentro()
        {
            await _asyncDentroDeAsync.ExecutarAsync();
            Console.WriteLine("Operação concluída \n\n");
        }

        public async Task EnviandoEmails()
        {
            await _envioEmails.Enviar();
            Console.WriteLine("Operação concluída \n\n");
        }

        public async Task EnviandoEmailsComExcecaoNoMeio()
        {
            await _envioEmails.EnviandoComExcecaoNoMeio();
            Console.WriteLine("Operação concluída \n\n");
        }

        public async Task ConsultandoServicosEFicandoComPrimeiro()
        {
            await _consultandoServicos.Consultar();
            Console.WriteLine("Operação concluída \n\n");
        }

        public async Task ConsultandoServicosTratandoQuemResponde()
        {
            await _consultandoServicos.ConsultandoServicosETratandoConformeRespondem();
            Console.WriteLine("Operação concluída \n\n");
        }

        public async Task ConsultandoServicosComExcecaoNoMeio()
        {
            await _consultandoServicos.ConsultarServicoComException();
            Console.WriteLine("Operação concluída \n\n");
        }
    }
}