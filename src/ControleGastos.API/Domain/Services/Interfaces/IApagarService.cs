using ControleGastos.API.Contracts.Apagar;
using static ControleGastos.API.Domain.Services.Interfaces.IService;

namespace ControleGastos.API.Domain.Services.Interfaces;

public interface IApagarService : IService<ApagarRequestContract, ApagarResponseContract, long> {

    Task<IEnumerable<ApagarResponseContract>> GetByIdUsuario(long id, long idUsuario);

}
