using ControleGastos.API.Contracts.Titulo;

namespace ControleGastos.API.Contracts.Apagar
{
    public class ApagarRequestContract : TituloRequestContract {

        public double ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }

    }
}
