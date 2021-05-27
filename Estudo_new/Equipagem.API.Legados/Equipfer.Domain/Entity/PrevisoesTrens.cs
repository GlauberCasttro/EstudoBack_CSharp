using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Equipfer.Domain.Entity
{
    [DataContract(Name ="previsoesTrens")]
    public class PrevisoesTrens
    {
        public PrevisoesTrens()
        {
            Trens = new List<TremPrevisao>();
        }
        [DataMember(Name = "snapshot")]
        public string Snapshot { get; set; }
        [DataMember(Name = "trens")]
        public IList<TremPrevisao> Trens { get; set; }
    }
    [DataContract(Name = "tremPrevisao")]
    public class TremPrevisao
    {
        [DataMember(Name = "os")]
        public int Os { get; set; }
        [DataMember(Name = "prefixo")]
        public string Prefixo { get; set; }
        [DataMember(Name = "origem")]
        public string Origem { get; set; }
        [DataMember(Name = "destino")]
        public string Destino { get; set; }
        [DataMember(Name = "segmentos")]
        public IList<Segmento> Segmentos { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
    [DataContract(Name = "segmento")]
    public class Segmento
    {
        [DataMember(Name = "codigo")]
        public string Codigo { get; set; }
        [DataMember(Name = "dataHora")]
        public string DataHora { get; set; }
        [DataMember(Name = "planejado")]
        public bool Planejado { get; set; }
    }
}

