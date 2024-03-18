using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.Domain.Repositories.Interfaces;

public interface IAreceberRepository : IRepository<Areceber, long>
{

    // Esse método vai pegar todas as naturezas vinculadas ao id do usuário passado 
    Task<IEnumerable<Areceber>> GetByIdUsuario(long idUsuario);


}
