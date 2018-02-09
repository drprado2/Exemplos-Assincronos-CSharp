using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstudoTask
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var executor = new Executor();
            var teclaApertada = "";
            do
            {
                Console.WriteLine("*********************************");
                Console.WriteLine("Selecione uma das opções");
                Console.WriteLine("*********************************");

                Console.WriteLine("1 - Criar arquivos texto com await e sem await \n");

                Console.WriteLine("2 - Testando cancelamento \n");

                Console.WriteLine("3 - Consultando operação assíncrona com outra dentro \n");

                Console.WriteLine("4 - Enviando e-mails \n");

                Console.WriteLine("5 - Enviando e-mails com exceção no meio \n");

                Console.WriteLine("6 - Consultando serviços e ficando com o primeiro \n");

                Console.WriteLine("7 - Consultando serviços e tratando conforme respondem \n");

                Console.WriteLine("8 - Consultando serviços e tratando exceção \n");

                Console.WriteLine("9 - Loops paralelos \n");

                Console.WriteLine("0 - cancelar \n");

                teclaApertada = Console.ReadLine();

                switch (teclaApertada)
                {
                    case "1":
                        executor.CriarArquivoTextoComAwaitSemAwait();
                        break;
                    case "2":
                        executor.TestandoCancelamento();
                        break;
                    case "3":
                        executor.ConsultandoOperacaoAssincronaComOutraDentro();
                        break;
                    case "4":
                        executor.EnviandoEmails();
                        break;
                    case "5":
                        executor.EnviandoEmailsComExcecaoNoMeio();
                        break;
                    case "6":
                        executor.ConsultandoServicosEFicandoComPrimeiro();
                        break;
                    case "7":
                        executor.ConsultandoServicosTratandoQuemResponde();
                        break;
                    case "8":
                        executor.ConsultandoServicosComExcecaoNoMeio();
                        break;
                    case "9":
                        executor.LoopsParalelos();
                        continue;
                    default:
                        Console.WriteLine("\n\n xxx - Opção inválida - xxx \n\n");
                        break;
                }
            }
            while (teclaApertada != "0");
        }
    }
}