using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.Domain.Repositories.Interfaces;

public interface IApagarRepository : IRepository<Apagar, long>
{

    // Esse método vai pegar todas as naturezas vinculadas ao id do usuário passado 
    Task<IEnumerable<Apagar>> GetByIdUsuario(long idUsuario);


}
