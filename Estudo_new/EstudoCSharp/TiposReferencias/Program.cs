using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiposReferencias
{
    class Program
    {
        static void Main(string[] args)
        {
            //memorias stack/heap
            Produto p1, p2;//ainda nao existe na memorias

            p1 = new Produto(); //criou uma alocação na memoria heap e um apontamentp de p1 para produto

            p2 = p1; // estao apontando para o mesmo local na memoria heap

            int x, y; // sao criadas apenaas na memoria stack

            x = 10;
            y = x; // as duas variaveis assumiram o valor 10, pois na memoria cria um novo valor

            Point p; //struct nao precisa ser iniciado. pois sao variaveis do tipo valor e estao armazenadas na memoria heap
            p.a = 10;
            p.b = 11;

            Console.WriteLine(p);
            Console.ReadKey();

        }

        class Produto
        {

        }
        public struct Point
        {
            public int a;
            public int b;

            public override string ToString()
            {
                return ($"numeros:  {a} {b}");
            }

        }
    }
}
