using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nexo.Models
{
    public class FornecedorModel
    {
        [Key]
        public int ID_Fornecedor { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime Data_Cadastro { get; set; }

        public ICollection<ProdutoModel> Produtos { get; set; }

        public virtual int StatusId
        {
            get
            {
                return (int)this.Status;
            }
            set
            {
                Status = (TiposStatus)value;
            }
        }
        [EnumDataType(typeof(TiposStatus))]
        public TiposStatus Status { get; set; }
    }


    public enum TiposStatus
    {
        NA, Prata, Ouro, Platina, Diamante
    }
}