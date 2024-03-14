using ControleGastos.API.Contracts.NaturezaDeLancamento;
using static ControleGastos.API.Domain.Services.Interfaces.IService;

namespace ControleGastos.API.Domain.Services.Interfaces;

public interface INaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestContract, NaturezaDeLancamentoResponseContract, long> {

    Task<IEnumerable<NaturezaDeLancamentoResponseContract>> GetByIdUsuario(long id, long idUsuario);

}
