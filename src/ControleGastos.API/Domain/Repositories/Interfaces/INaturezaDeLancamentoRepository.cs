using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.Domain.Repositories.Interfaces;

public interface INaturezaDeLancamentoRepository : IRepository<NaturezaDeLancamento, long>
{

    // Esse método vai pegar todas as naturezas vinculadas ao id do usuário passado 
    Task<IEnumerable<NaturezaDeLancamento>> GetByIdUsuario(long idUsuario);


}
