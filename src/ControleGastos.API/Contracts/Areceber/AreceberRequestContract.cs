using ControleGastos.API.Contracts.Titulo;

namespace ControleGastos.API.Contracts.Areceber
{
    public class AreceberRequestContract : TituloRequestContract {

        public double ValorRecebido { get; set; }
        public DateTime DataRecebimento { get; set; }

    }
}
