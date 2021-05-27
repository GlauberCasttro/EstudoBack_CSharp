using Equipfer.Domain.MacroAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipfer.Domain.Entity.MacroAggregate
{
    public class Macro
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public char Prefixo { get; set; }
        public char CodigoAtividade { get; set; }
        public string Matricula { get; set; }
        public DateTime InicioAtividade { get; set; }
        public int TipoMacro { get; set; }
       // public List<ErroMacro> ErrosMacro { get; set; }





    }
}
