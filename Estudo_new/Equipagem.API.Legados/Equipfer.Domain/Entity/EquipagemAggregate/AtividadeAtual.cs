namespace Equipfer.Domain.Entity.EquipagemAggregate
{
    public class AtividadeAtual
    {
        public string Codigo { get; set; }
        public string Atividade { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public Trem Trem { get; set; }
        public bool IsTrabalalho { get; set; }
    }
}