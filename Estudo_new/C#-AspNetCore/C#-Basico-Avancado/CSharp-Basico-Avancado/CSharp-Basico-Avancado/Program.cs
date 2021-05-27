using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basico_Avancado
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //double SumNumbers(List<double[]> setsOfNumbers, int indexOfSetToSum)
            //{
            //    //trabalhando com valor null setsOfNumbers?[indexOfSetToSum]? e retorno NaN
            //    return setsOfNumbers?[indexOfSetToSum]?.Sum() ?? double.NaN;
            //}

            //var sum1 = SumNumbers(null, 0);
            //Console.WriteLine(sum1);  // output: NaN


            ////lista de array de double
            //var numberSets = new List<double[]>
            //        {
            //            new[] { 1.0, 2.0, 3.0 },
            //            new [] {2.0, 3.0 }
            //        };

            //var sum2 = SumNumbers(numberSets, 0);
            //Console.WriteLine(sum2);  // output: 6

            //var sum3 = SumNumbers(numberSets, 1);
            //Console.WriteLine(sum3);  // output: NaN
            //Console.ReadKey();


            //ProblemaCalculoMaiorArea p = new ProblemaCalculoMaiorArea(10.0, 12.0, 13.0);

            //ProblemaCalculoMaiorArea p2 = new ProblemaCalculoMaiorArea(11.0, 12.0, 13.0);

            //var area1 = p.CalculaArea();
            //var area2 = p2.CalculaArea();

            //if (area1 == area2)
            //{
            //    Console.WriteLine($"as area dos tringulos são iguais!!! triangulo 1 = {area1} triangulo 2 = {area2}");
            //}
            //else if (area1 > area2)
            //{
            //    Console.WriteLine($"a area do tringulo 1 é maior. area = {area1}");
            //}
            //else
            //{
            //    Console.WriteLine($"a area do tringulo 2 é maior. area = {area2}");
            //}


            Produto produto1 = new Produto
            {
                Nome = "Celular",
                Preco = 1253.25,
                QuantidadeEstoque = 2
            };
            Console.WriteLine($"Produto :{produto1}");
            Console.WriteLine("Valor total em estoque: " + produto1.ValorTotalEstoque());
            Console.WriteLine("Removendo produtos: ");
            produto1.RemoverProdutos(1);
            Console.WriteLine("Quantidade estoque: ");
            Console.WriteLine($"Produto :{produto1}");
            Console.WriteLine("Valor total em estoque: " + produto1.ValorTotalEstoque());

            Console.ReadKey();

        }
    }
}
