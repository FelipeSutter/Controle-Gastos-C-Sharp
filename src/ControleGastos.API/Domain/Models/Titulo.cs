using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.Domain.Models;

public abstract class Titulo {

    [Key]
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
    public string Descricao { get; set; } = string.Empty;

    public string? Observacao { get; set; } = string.Empty;

    [Required]
    public long IdUsuario { get; set; }

    public Usuario? Usuario { get; set; } // de quem é o titulo a pagar

    [Required]
    public long IdNatureza { get; set; }

    public NaturezaDeLancamento? Natureza { get; set; } // qual a natureza desse titulo a pagar

    [Required(ErrorMessage = "O campo de ValorOriginal é obrigatório.")]
    public double ValorOriginal { get; set; }


    [Required]
    public DateTime DataCadastro { get; set; }

    [Required(ErrorMessage = "O campo de DataVencimento é obrigatório.")]
    public DateTime DataVencimento { get; set; }

    public DateTime? DataInativacao { get; set; }

    public DateTime? DataReferencia { get; set; }


}
