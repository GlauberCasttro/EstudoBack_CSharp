using System;

namespace ExecicioFixacaoVetor
{
    class Program
    {
        static void Main(string[] args)
        {
            AlugarQuarto al = new AlugarQuarto();


            Console.WriteLine("Deseja verificar o status do quarto?");
            var statusQuarto = char.Parse(Console.ReadLine() ?? string.Empty);

            if(statusQuarto == 'S' || statusQuarto == 's')
            {
                al.ConsultarStatusQuartos();
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Quantos quartos deseja alugar?");
            var numerosAlugar = int.Parse(Console.ReadLine() ?? string.Empty);
            al.VerificarQuartosDisponiveis();
            Console.WriteLine("------------------Somente esses disponiveis-------------------------------");
            for (int i = 0; i < numerosAlugar; i++)
            {
                
                Console.WriteLine("Digite seu nome!!!");
                var Nome = Console.ReadLine();

                Console.WriteLine("Digite seu email!!!");
                var email = Console.ReadLine();
              
                

                Console.WriteLine("Digite o numero do quarto que deseja!!!");
                var numeroQuarto = int.Parse(Console.ReadLine() ?? string.Empty);

                if(!al.AlugarQuartoPessoa(new Pessoa(Nome, email), numeroQuarto))
                {
                    Console.WriteLine("Quarto Alugado...");
                }
            }
            al.VerificarQuartosDisponiveis();
            Console.ReadKey();

        }
    }
}
