using AutoMapper;
using ControleGastos.API.Contracts.NaturezaDeLancamento;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;

namespace ControleGastos.API.Domain.Services.Classes;

public class NaturezaDeLancamentoService : INaturezaDeLancamentoService {

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;
    private readonly IMapper _mapper;

    public NaturezaDeLancamentoService(INaturezaDeLancamentoRepository naturezaDeLancamentoRepository, IMapper mapper, 
        IUsuarioRepository usuarioRepository) {
        _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> GetAll(long idUsuario) {
        var naturezaDeLancamentos = await _naturezaDeLancamentoRepository.GetByIdUsuario(idUsuario);
        return naturezaDeLancamentos.Select(u => _mapper.Map<NaturezaDeLancamentoResponseContract>(u));
    }

    public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> GetByIdUsuario(long id, long idUsuario) {
        var naturezaDeLancamento = await GetByIdAndIdUsuario(id, idUsuario);

        // Esse método está bugado

        var naturezaDeLancamentos = await _naturezaDeLancamentoRepository.GetByIdUsuario(id);
        return naturezaDeLancamentos.Select(n => _mapper.Map<NaturezaDeLancamentoResponseContract>(n));

    }

    public async Task<NaturezaDeLancamentoResponseContract> GetById(long id, long idUsuario) {
        var naturezaDeLancamento = await GetByIdAndIdUsuario(id, idUsuario);

        return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
    }

    public async Task<NaturezaDeLancamentoResponseContract> Add(NaturezaDeLancamentoRequestContract request, long idUsuario) {
        var naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(request);

        naturezaDeLancamento.DataCadastro = DateTime.Now;
        naturezaDeLancamento.IdUsuario = idUsuario;

        naturezaDeLancamento = await _naturezaDeLancamentoRepository.Add(naturezaDeLancamento);

        return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
    }

    public async Task<NaturezaDeLancamentoResponseContract> Update(long id, NaturezaDeLancamentoRequestContract request, long idUsuario) {
        
        var naturezaDeLancamento = await GetByIdAndIdUsuario(id, idUsuario);

        naturezaDeLancamento.Descricao = request.Descricao;
        naturezaDeLancamento.Observacao = request.Observacao;

        naturezaDeLancamento = await _naturezaDeLancamentoRepository.Update(naturezaDeLancamento);

        return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);

    }

    public async Task Delete(long id, long idUsuario) {

        var naturezaDeLancamento = await GetByIdAndIdUsuario(id, idUsuario);

        await _naturezaDeLancamentoRepository.Delete(_mapper.Map<NaturezaDeLancamento>(naturezaDeLancamento));
    }

    private async Task<NaturezaDeLancamento> GetByIdAndIdUsuario(long id, long idUsuario) {
        var naturezaDeLancamento = await _naturezaDeLancamentoRepository.GetById(id);
        
        if (naturezaDeLancamento == null || naturezaDeLancamento.IdUsuario != idUsuario) {
            throw new Exception($"Não foi encontrada nenhuma natureza pelo id {id}");
        }

        return naturezaDeLancamento;

    }

}
