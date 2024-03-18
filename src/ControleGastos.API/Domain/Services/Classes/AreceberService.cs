using AutoMapper;
using ControleGastos.API.Contracts.Areceber;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;

namespace ControleGastos.API.Domain.Services.Classes;

public class AreceberService : IAreceberService {

    private readonly IAreceberRepository _areceberRepository;
    private readonly IMapper _mapper;

    public AreceberService(IAreceberRepository areceberRepository, IMapper mapper) {
        _areceberRepository = areceberRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AreceberResponseContract>> GetAll(long idUsuario) {
        var areceber = await _areceberRepository.GetByIdUsuario(idUsuario);
        return areceber.Select(u => _mapper.Map<AreceberResponseContract>(u));
    }

    public async Task<IEnumerable<AreceberResponseContract>> GetByIdUsuario(long id, long idUsuario) {
        var areceber = await GetByIdAndIdUsuario(id, idUsuario);

        // Esse método está bugado

        var arecebers = await _areceberRepository.GetByIdUsuario(id);
        return arecebers.Select(n => _mapper.Map<AreceberResponseContract>(n));

    }

    public async Task<AreceberResponseContract> GetById(long id, long idUsuario) {
        var areceber = await GetByIdAndIdUsuario(id, idUsuario);

        return _mapper.Map<AreceberResponseContract>(areceber);
    }

    public async Task<AreceberResponseContract> Add(AreceberRequestContract request, long idUsuario) {
        var areceber = _mapper.Map<Areceber>(request);

        areceber.DataCadastro = DateTime.Now;
        areceber.IdUsuario = idUsuario;

        areceber = await _areceberRepository.Add(areceber);

        return _mapper.Map<AreceberResponseContract>(areceber);
    }

    public async Task<AreceberResponseContract> Update(long id, AreceberRequestContract request, long idUsuario) {
        
        var areceber = await GetByIdAndIdUsuario(id, idUsuario);

        // Dessa forma coloca o que não quer que atualize
        var contrato = _mapper.Map<Areceber>(request);
        contrato.IdUsuario = areceber.IdUsuario;
        contrato.Id = areceber.Id;
        contrato.DataCadastro = areceber.DataCadastro;


        /*  Dá pra fazer dessa forma, que coloca o que quer atualizar
      
            areceber.Descricao = request.Descricao;
            areceber.Observacao = request.Observacao;
            areceber.ValorOriginal = request.ValorOriginal;
            areceber.ValorRecebido = request.ValorRecebido;
            areceber.DataRecebimento = request.DataRecebimento;
            areceber.DataReferencia = request.DataReferencia;
            areceber.DataVencimento = request.DataVencimento;
            areceber.IdNatureza = request.IdNatureza;
        */

        areceber = await _areceberRepository.Update(contrato);

        return _mapper.Map<AreceberResponseContract>(contrato);

    }

    public async Task Delete(long id, long idUsuario) {

        var areceber = await GetByIdAndIdUsuario(id, idUsuario);

        await _areceberRepository.Delete(_mapper.Map<Areceber>(areceber));
    }

    private async Task<Areceber> GetByIdAndIdUsuario(long id, long idUsuario) {
        var areceber = await _areceberRepository.GetById(id);
        
        if (areceber == null || areceber.IdUsuario != idUsuario) {
            throw new Exception($"Não foi encontrado nenhumn titulo a receber pelo id {id}");
        }

        return areceber;

    }

}
