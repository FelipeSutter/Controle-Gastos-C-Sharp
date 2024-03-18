namespace ControleGastos.API.Contracts.Areceber
{
    public class AreceberResponseContract : AreceberRequestContract
    {

        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataInativacao { get; set; }

    }
}
