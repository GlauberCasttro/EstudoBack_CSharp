using System;

namespace Equipfer.Domain.Entity.EquipagemAggregate
{
    public class Indisponibilidade
    {
        public string Codigo { get; set; }
        public string Atividade { get; set; }
        public DateTime DataInicio { get; set; }
        public Nullable<DateTime> DataTermino { get; set; }
        public string LocalTermino { get; set; }
    }
}