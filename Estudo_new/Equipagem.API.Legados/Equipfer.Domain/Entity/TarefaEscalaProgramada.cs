using System;

namespace Equipfer.Domain.Entity
{
    public class TarefaEscalaProgramada
    {
        public string Codigo { get; set; }
        public Nullable<DateTime> DataInicio { get; set; }
        public Nullable<DateTime> DataFim { get; set; }
        public DateTime DataProgramada { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
    }
}
