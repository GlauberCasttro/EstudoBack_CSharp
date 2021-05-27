using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basico_Avancado
{
    public class ProblemaCalculoMaiorArea
    {
        public double LadoA { get; set; }
        public double LadoB { get; set; }
        public double LadoC { get; set; }
        public double P { get; private set; }
        public double Area { get; private set; }


        public ProblemaCalculoMaiorArea(double a, double b, double c)
        {
            LadoA = a;
            LadoB = b;
            LadoC = c;
        }

        private void CalculaP()
        {
            P = (LadoA + LadoB + LadoC) / 2;
        }
        public double CalculaArea()
        {
            CalculaP();
            Area = Math.Sqrt(P*(P-LadoA)*(P-LadoB)*(P-LadoC));
            return Area;
        }

    }
}
