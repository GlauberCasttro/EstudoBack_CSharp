using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DEMO_DDD.APPLICATION.ViewModels
{
    public class ClienteViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}.")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} a {1} caracteres.", MinimumLength = 2)]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
