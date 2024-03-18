namespace ControleGastos.API.Contracts.Apagar
{
    public class ApagarResponseContract : ApagarRequestContract
    {

        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }

    }
}
