using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstudoTask.Operacoes
{
    public class ExecutandoOperacaoAssincronaComOutraAssincronaDentro
    {
        public async Task ExecutarAsync()
        {
            var retorno = await Task.Run(async () =>
            {
                var textoRetornar = "";
                textoRetornar += await ExecutarInterno();
                textoRetornar += await ExecutarInterno();
                textoRetornar += await ExecutarInterno();
                return textoRetornar;
            });

            var retornoNovo = await Task.Run(() =>
            {
                var textoRetornar = "";
                textoRetornar += ExecutarInterno();
                textoRetornar += ExecutarInterno();
                textoRetornar += ExecutarInterno();
                return textoRetornar;
            });

            var threadComRetornoJaPronto = Task.FromResult("Já ta pronto");

            var retornoTerceiro = await threadComRetornoJaPronto;

            var textoFinal = retorno + retornoNovo;
        }

        private async Task<string> ExecutarInterno()
        {
            Thread.Sleep(3000);
            return "Conclui a execução";
        }
    }
}
