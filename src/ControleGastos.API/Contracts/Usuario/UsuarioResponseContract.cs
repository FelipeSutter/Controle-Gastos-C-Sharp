namespace ControleGastos.API.Contracts.Usuario;

// Verificar se essa herança está certa mesmo, nesse caso era para importar de UsuarioLoginResponseContract
public class UsuarioResponseContract : UsuarioRequestContract {
    public long Id { get; set; }
    public DateTime DataCadastro { get; set; }
}
