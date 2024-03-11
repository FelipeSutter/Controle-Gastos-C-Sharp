using ControleGastos.API.Contracts.Usuario;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;

namespace ControleGastos.API.Domain.Services.Classes;

public class UsuarioService : IUsuarioService {

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository) => _usuarioRepository = usuarioRepository;
    
    public Task<UsuarioResponseContract> Add(UsuarioRequestContract request, long idUsuario) {
        throw new NotImplementedException();
    }

    public Task<UsuarioLoginResponseContract> Authenticate(UsuarioLoginRequestContract usuario) {
        throw new NotImplementedException();
    }

    public Task Delete(long id, long idUsuario) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UsuarioResponseContract>> GetAll(long idUsuario) {
        throw new NotImplementedException();
    }

    public Task<UsuarioResponseContract> GetById(long id, long idUsuario) {
        throw new NotImplementedException();
    }

    public Task<UsuarioResponseContract> Update(long id, UsuarioRequestContract request, long idUsuario) {
        throw new NotImplementedException();
    }
}
