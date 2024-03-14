using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.Domain.Models {
    public class NaturezaDeLancamento {

        [Key]
        public long Id { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        public Usuario Usuario { get; set; } // para fazer o relacionamento 

        [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
        public string Descricao { get; set; } = string.Empty;

        public string? Observacao { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set;}

        public DateTime? DataInativacao { get; set;}
    }
}