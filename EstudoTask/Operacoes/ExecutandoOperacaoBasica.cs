using System;
using System.IO;
using System.Threading;
// O Namespace System.Threading.Tasks é o nam
using System.Threading.Tasks;

namespace EstudoTask.Operacoes
{
    public class ExecutandoOperacaoBasica
    {
        // Todo método assíncrono deve possuir o sufixo Async em seu nome
        // Todo método assíncrono deve retornar um Task ou Task<TResult>
        // Todo método assíncrono deve possuir a palavra reservada async
        public async Task<string> ExecutarAsync()
        {
            // Podemos ter parte do código sendo executado de forma síncrona
            var abc = 25 * 10 + 5;
                        // o método Task.Run() faz a execução do código assíncrono, criando uma nova thread para executar o código aqui dentro.
                        // a palavra await aqui faz com que a thread que chamou o Task.Run() aguarde a sincronização da nova thread para então continuar executando
            await Task.Run(() => ExecutarOperacao(4000));
                        // Nesse caso a baixo será criada a nova thread para executar o código, porém o código continuará executando independente do código dessa
                        // nova thread concluir.
            var resultB = Task.Run(() => ExecutarOperacao(10000));

            return "Concluiu métodos";
        }

        public void CancelandoTask(CancellationToken token)
        {
            var resultB = Task.Run(() => 
            {
                Thread.Sleep(15000);
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                var diretorioAtual = Directory.GetCurrentDirectory();
                var textoGravar = $"Concluiu 1 thread em {DateTime.Now}";
                var arquivoCriar = $"ArquivoCriado.{Guid.NewGuid()}.txt";
                File.WriteAllText($"{diretorioAtual}\\{arquivoCriar}", textoGravar);
            }, token);
        }

        private void ExecutarOperacao(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
            var diretorioAtual = Directory.GetCurrentDirectory();
            var textoGravar = $"Concluiu 1 thread em {DateTime.Now}";
            var arquivoCriar = $"ArquivoCriado.{Guid.NewGuid()}.txt";
            File.WriteAllText($"{diretorioAtual}\\{arquivoCriar}", textoGravar);
        }

        private async Task ExecutarOperacaoAsync(int miliSeconds, CancellationToken token = new CancellationToken())
        {
            Thread.Sleep(miliSeconds);
            var diretorioAtual = Directory.GetCurrentDirectory();
            var textoGravar = $"Concluiu 1 thread em {DateTime.Now}";
            var arquivoCriar = $"ArquivoCriado.{Guid.NewGuid()}.txt";
            File.WriteAllText($"{diretorioAtual}\\{arquivoCriar}", textoGravar);
        }
    }
}