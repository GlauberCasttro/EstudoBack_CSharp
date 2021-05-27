using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Formularios.Models
{
    /// <summary>
    /// Documentação annotations
    /// https://docs.microsoft.com/pt-br/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1
    /// </summary>
    public class Filme
    {

     
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigátorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} precisa ter entre {2} e {1} caracteres...")]
        public string Titulo { get; set; }



        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "O campo {0} é obrigátorio")]
        [Display(Name = "Data de Lançamento")]
        public DateTime DataLancamento { get; set; }



        [RegularExpression(@"^[A-Z]+[a-zA-Z\u00C0-\u00FF""'\w-]*$", ErrorMessage = "Formato de Gênero invalido")]//, ErrorMessage ="Formato inválido.")]
        [StringLength(30, ErrorMessage = "Maximo de {1} caracteres"),Required(ErrorMessage ="O campo {0} é obrigátorio")]//forma de sobrecarga de annotations
       [DisplayName("Gênero")]
        public string Genero { get; set; }

        [Range(1, 1000)]
        [Required(ErrorMessage = "Campo {0} obrigátorioo")]
        [Column(TypeName = "decimal(18,2")]//annotations para valor de campo no banco
        public decimal Valor { get; set; }


        [RegularExpression(@"^[0-5]*$", ErrorMessage ="Numeros somente de 1 a 5.")]
        [DisplayName("Avaliação")]
        [Required(ErrorMessage = "Preencha o campo avaliação")]
        public int Avaliacao { get; set; }
    }
}
