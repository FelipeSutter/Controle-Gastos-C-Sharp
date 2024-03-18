using AutoMapper;
using ControleGastos.API.Contracts.Apagar;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;

namespace ControleGastos.API.Domain.Services.Classes;

public class ApagarService : IApagarService {

    private readonly IApagarRepository _apagarRepository;
    private readonly IMapper _mapper;

    public ApagarService(IApagarRepository apagarRepository, IMapper mapper) {
        _apagarRepository = apagarRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ApagarResponseContract>> GetAll(long idUsuario) {
        var apagar = await _apagarRepository.GetByIdUsuario(idUsuario);
        return apagar.Select(u => _mapper.Map<ApagarResponseContract>(u));
    }

    public async Task<IEnumerable<ApagarResponseContract>> GetByIdUsuario(long id, long idUsuario) {
        var apagar = await GetByIdAndIdUsuario(id, idUsuario);

        // Esse método está bugado

        var apagars = await _apagarRepository.GetByIdUsuario(id);
        return apagars.Select(n => _mapper.Map<ApagarResponseContract>(n));

    }

    public async Task<ApagarResponseContract> GetById(long id, long idUsuario) {
        var apagar = await GetByIdAndIdUsuario(id, idUsuario);

        return _mapper.Map<ApagarResponseContract>(apagar);
    }

    public async Task<ApagarResponseContract> Add(ApagarRequestContract request, long idUsuario) {
        var apagar = _mapper.Map<Apagar>(request);

        apagar.DataCadastro = DateTime.Now;
        apagar.IdUsuario = idUsuario;

        apagar = await _apagarRepository.Add(apagar);

        return _mapper.Map<ApagarResponseContract>(apagar);
    }

    public async Task<ApagarResponseContract> Update(long id, ApagarRequestContract request, long idUsuario) {
        
        var apagar = await GetByIdAndIdUsuario(id, idUsuario);

        // Dessa forma coloca o que não quer que atualize
        var contrato = _mapper.Map<Apagar>(request);
        contrato.IdUsuario = apagar.IdUsuario;
        contrato.Id = apagar.Id;
        contrato.DataCadastro = apagar.DataCadastro;
       
        
        /*  Dá pra fazer dessa forma, que coloca o que quer atualizar
      
            apagar.Descricao = request.Descricao;
            apagar.Observacao = request.Observacao;
            apagar.ValorOriginal = request.ValorOriginal;
            apagar.ValorPago = request.ValorPago;
            apagar.DataPagamento = request.DataPagamento;
            apagar.DataReferencia = request.DataReferencia;
            apagar.DataVencimento = request.DataVencimento;
            apagar.IdNatureza = request.IdNatureza;
        */

        apagar = await _apagarRepository.Update(contrato);

        return _mapper.Map<ApagarResponseContract>(contrato);

    }

    public async Task Delete(long id, long idUsuario) {

        var apagar = await GetByIdAndIdUsuario(id, idUsuario);

        await _apagarRepository.Delete(_mapper.Map<Apagar>(apagar));
    }

    private async Task<Apagar> GetByIdAndIdUsuario(long id, long idUsuario) {
        var apagar = await _apagarRepository.GetById(id);
        
        if (apagar == null || apagar.IdUsuario != idUsuario) {
            throw new Exception($"Não foi encontrado nenhumn titulo a pagar pelo id {id}");
        }

        return apagar;

    }

}
