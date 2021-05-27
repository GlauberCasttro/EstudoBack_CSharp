using Equipfer.Domain.Entity.EquipagemAggregate;
using System;

namespace Equipfer.Domain.Entity
{
    public class AtividadeJornada
    {
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public Nullable<DateTime> Fim { get; set; }
        public Trem Trem { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public bool IsTrabalho { get; set; }
        public string Codigo { get; set; }
        public long? HoraExtra { get; set; }
    }
}
