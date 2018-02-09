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
        public async Task<string> ExecutarAsync()
        {
            var retorno = await Task.Run(async () =>
            {
                var textoRetornar = "";
                textoRetornar += await ExecutarInterno();
                textoRetornar += await ExecutarInterno();
                textoRetornar += await ExecutarInterno();
                return textoRetornar;
            });

            return retorno;
        }

        private async Task<string> ExecutarInterno()
        {
            Thread.Sleep(3000);
            return "Conclui a execução ";
        }
    }
}
