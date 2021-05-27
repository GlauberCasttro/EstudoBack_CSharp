using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecicioFixacaoVetor
{
    public class Quarto
    {
        public StatusQuarto StatusQuarto { get; set; }
        public int NumeroDoQuarto { get; set; }
        public Pessoa Locatario { get; set; }


        public Quarto()
        {
            StatusQuarto = StatusQuarto.Vazio;
        }

        public void OcuparQuarto()
        {
            StatusQuarto = StatusQuarto.Alugado;
        }

        public void DesocuparQuarto()
        {
            StatusQuarto = StatusQuarto.Vazio;
        }
        public override string ToString()
        {
            return "Numero do quarto: " + NumeroDoQuarto
                + "\n" + "Status: " + StatusQuarto;
        }
    }
}
