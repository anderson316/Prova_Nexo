using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nexo.Models
{
    public class ProdutoModel
    {
        [Key]
        public int Id_Produto { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O Valor Não Pode Ser Menor Que {1}")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime Data_Cadastro { get; set; }

        [ForeignKey("Fornecedor")]
        [Display(Name = "Identificador do Fornecedor")]
        public int ID_Fornecedor { get; set; }

        public FornecedorModel Fornecedor { get; set; }
    }
}
