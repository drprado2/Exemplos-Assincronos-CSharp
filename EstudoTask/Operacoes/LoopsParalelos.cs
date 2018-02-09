using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace EstudoTask.Operacoes
{
    public class LoopsParalelos
    {
        public void ProcessarLista()
        {
            var lista = new List<Pessoa>()
            {
                new Pessoa("pedro"),
                new Pessoa("paulo"),
                new Pessoa("joao")
            };

            var dataInicio = DateTime.Now;

            Parallel.ForEach(lista, ProcessarItem);

            // Comentar com parallel e testar de maneira síncrona para comparar
            // lista.ForEach(ProcessarItem);

            var dataFinal = DateTime.Now;

            var tempoPercorrido = (dataFinal - dataInicio).TotalSeconds;

            var itens = "";

            lista.ForEach(x => itens += (x.Nome + " "));

            Console.WriteLine($"Concluiu o loop tempo total: {tempoPercorrido}\n Nomes: {itens}");

        }

        private void ProcessarItem(Pessoa pessoa)
        {
            Thread.Sleep(5000);
            pessoa.Nome = pessoa.Nome.ToUpper();
        }
    }

    class Pessoa
    {
        public Pessoa(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
