using System;
using System.Collections.Generic;
using System.Text;

namespace Equipfer.Domain.Entity
{
    public class JornadaMensalAcumulada
    {
        public long HoraExtraMensal { get; set; }
        public long JornadaEfetivaMensal { get; set; }
        public long JornadaEfetivaNortunaQuizenal { get; set; }
        public long JornadaEfetivaQuizenal { get; set; }
        public long JornadaEfetivaSemanal { get; set; }
        public bool IsJornadaNoiteAnterior { get; set; }
    }
}
