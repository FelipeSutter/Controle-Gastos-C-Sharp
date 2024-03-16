using AutoMapper;
using ControleGastos.API.Contracts.Apagar;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;

namespace ControleGastos.API.Domain.Services.Classes;

public class ApagarService : IApagarService {

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IApagarRepository _apagarRepository;
    private readonly IMapper _mapper;

    public ApagarService(IApagarRepository apagarRepository, IMapper mapper, 
        IUsuarioRepository usuarioRepository) {
        _apagarRepository = apagarRepository;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<ApagarResponseContract>> GetAll(long idUsuario) {
        var apagar = await _apagarRepository.GetByIdUsuario(idUsuario);
        return apagar.Select(u => _mapper.Map<ApagarResponseContract>(u));
    }

    public async Task<IEnumerable<ApagarResponseContract>> GetByIdUsuario(long id, long idUsuario) {
        var apagar = await GetByIdAndIdUsuario(id, idUsuario);

        // Esse método está bugado

        var apagar = await _apagarRepository.GetByIdUsuario(id);
        return apagar.Select(n => _mapper.Map<ApagarResponseContract>(n));

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

        apagar.Descricao = request.Descricao;
        apagar.Observacao = request.Observacao;

        apagar = await _apagarRepository.Update(apagar);

        return _mapper.Map<ApagarResponseContract>(apagar);

    }

    public async Task Delete(long id, long idUsuario) {

        var apagar = await GetByIdAndIdUsuario(id, idUsuario);

        await _apagarRepository.Delete(_mapper.Map<Apagar>(apagar));
    }

    private async Task<Apagar> GetByIdAndIdUsuario(long id, long idUsuario) {
        var apagar = await _apagarRepository.GetById(id);
        
        if (apagar == null || apagar.IdUsuario != idUsuario) {
            throw new Exception($"Não foi encontrada nenhuma natureza pelo id {id}");
        }

        return apagar;

    }

}
