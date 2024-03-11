using ControleGastos.API.Contracts.Usuario;
using static ControleGastos.API.Domain.Services.Interfaces.IService;

namespace ControleGastos.API.Domain.Services.Interfaces;

public interface IUsuarioService : IService<UsuarioRequestContract, UsuarioResponseContract, long> {

    Task<UsuarioLoginResponseContract> Authenticate(UsuarioLoginRequestContract usuario);

}
