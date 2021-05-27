namespace Equipfer.Domain.Entity
{
    public class Atividade
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public bool IsTrabalho { get; internal set; }
    }
}
