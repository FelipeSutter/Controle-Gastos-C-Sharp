using ControleGastos.API.Contracts.Areceber;
using static ControleGastos.API.Domain.Services.Interfaces.IService;

namespace ControleGastos.API.Domain.Services.Interfaces;

public interface IAreceberService : IService<AreceberRequestContract, AreceberResponseContract, long> {

    Task<IEnumerable<AreceberResponseContract>> GetByIdUsuario(long id, long idUsuario);

}
