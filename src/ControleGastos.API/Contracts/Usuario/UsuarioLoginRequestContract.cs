namespace ControleGastos.API.Contracts.Usuario;

public class UsuarioLoginRequestContract {

    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

}
