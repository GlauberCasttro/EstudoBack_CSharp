using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecicioFixacaoVetor
{
    public class AlugarQuarto
    {
        public int Numero { get; set; }
        public Pessoa Locatario { get; set; }
        public Quarto[] Quartos = new Quarto[10];

        public AlugarQuarto()
        {

            for (var i = 0; i < Quartos.Length; i++)
            {
                Quartos[i] = new Quarto
                {
                    NumeroDoQuarto = i + 1
                };
            }

        }

        public bool AlugarQuartoPessoa(Pessoa pessoa, int numeroQuartoDesejado)
        {
            if(pessoa != null && VerificarQuartoDisponivel(numeroQuartoDesejado))
            {
                Quartos[numeroQuartoDesejado].NumeroDoQuarto = numeroQuartoDesejado;
                Quartos[numeroQuartoDesejado].Locatario = pessoa;
                Quartos[numeroQuartoDesejado].StatusQuarto = StatusQuarto.Alugado;
                return true;
            }
            return false;
        }

        public void ConsultarStatusQuartos()
        {
            for (int i = 0; i < Quartos.Length; i++)
            {
                Console.WriteLine(Quartos[i]);
            }
        }

        public bool VerificarQuartoDisponivel(int numero)
        {
            if (Quartos[numero].StatusQuarto == StatusQuarto.Vazio)
            {
                return true;
            }
            return false;
        }

        public void VerificarQuartosDisponiveis()
        {
            for (int i = 0; i < Quartos.Length; i++)
            {
                if(Quartos[i].StatusQuarto == StatusQuarto.Vazio)
                {
                    Console.WriteLine(Quartos[i]);
                }
            }
        }
    }
}
