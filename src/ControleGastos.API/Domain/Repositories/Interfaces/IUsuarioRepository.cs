using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.Domain.Repositories.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario, long>
{

    Task<Usuario?> GetByEmail(string email);


}
