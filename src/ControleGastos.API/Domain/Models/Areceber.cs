using System.ComponentModel.DataAnnotations;

namespace ControleGastos.API.Domain.Models {
    public class Areceber : Titulo {

        [Required(ErrorMessage = "O campo de ValorRecebido é obrigatório.")]
        public double ValorRecebido { get; set; }

        public DateTime? DataRecebimento { get; set; }
    }
}