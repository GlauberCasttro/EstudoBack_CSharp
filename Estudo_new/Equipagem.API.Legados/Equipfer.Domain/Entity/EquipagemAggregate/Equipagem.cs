using System;

namespace Equipfer.Domain.Entity.EquipagemAggregate
{
    public class Equipagem
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Sede { get; set; }
        public string Telefone { get; set; }
        public string CodigoFerrovia { get; set; }
        public int JornadaDiaria { get; set; }
        public Indisponibilidade Indisponibilidade { get; set; }
        public EscalaProgramada EscalaProgramada { get; set; }
        public AtividadeAtual AtividadeAtual { get; set; }
        public Nullable<DateTime> ProximaFolga { get; set; }
        public int DuracaoUltimoDescanso { get; set; }
        public DateTime AberturaJornadaEfetiva { get; set; }
    }
}
