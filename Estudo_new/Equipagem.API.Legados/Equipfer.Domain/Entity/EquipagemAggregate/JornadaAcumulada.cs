using System;

namespace Equipfer.Domain.Entity.EquipagemAggregate
{
    public class JornadaAcumulada
    {
        public TimeSpan horaExtraMes { get; set; }
        public TimeSpan JornadaMes { get; set; }
        public TimeSpan FolgaMes { get; set; }
    }
}