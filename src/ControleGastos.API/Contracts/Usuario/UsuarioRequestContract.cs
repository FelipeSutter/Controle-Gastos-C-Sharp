namespace ControleGastos.API.Contracts.Usuario;

public class UsuarioRequestContract : UsuarioLoginRequestContract {

    public DateTime? DataInativacao { get; set; }

}
