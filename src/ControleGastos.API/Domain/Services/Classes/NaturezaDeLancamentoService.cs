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

    public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> GetAll() {
        var naturezaDeLancamentos = await _naturezaDeLancamentoRepository.GetAll();
        return naturezaDeLancamentos.Select(u => _mapper.Map<NaturezaDeLancamentoResponseContract>(u));
    }

    public async Task<IEnumerable<NaturezaDeLancamentoResponseContract>> GetByIdUsuario(long id) {
        var naturezaDeLancamentos = await _naturezaDeLancamentoRepository.GetByIdUsuario(id);
        return naturezaDeLancamentos.Select(n => _mapper.Map<NaturezaDeLancamentoResponseContract>(n));

    }

    public async Task<NaturezaDeLancamentoResponseContract> GetById(long id) {
        var naturezaDeLancamento = await _naturezaDeLancamentoRepository.GetById(id);
        return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
    }

    public async Task<NaturezaDeLancamentoResponseContract> Add(NaturezaDeLancamentoRequestContract request) {
        var naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(request);
        var usuario = _usuarioRepository.GetById(naturezaDeLancamento.IdUsuario);

        naturezaDeLancamento.DataCadastro = DateTime.Now;
        naturezaDeLancamento.IdUsuario = usuario.Id;

        naturezaDeLancamento = await _naturezaDeLancamentoRepository.Add(naturezaDeLancamento);

        return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);
    }

    public async Task<NaturezaDeLancamentoResponseContract> Update(long id, NaturezaDeLancamentoRequestContract request) {
        var naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(request);
        var usuario = _usuarioRepository.GetById(naturezaDeLancamento.IdUsuario);

        naturezaDeLancamento = await GetByIdAndIdUsuario(id, usuario.Id);

        naturezaDeLancamento.Descricao = request.Descricao;
        naturezaDeLancamento.Observacao = request.Observacao;

        naturezaDeLancamento = await _naturezaDeLancamentoRepository.Update(naturezaDeLancamento);

        return _mapper.Map<NaturezaDeLancamentoResponseContract>(naturezaDeLancamento);

    }

    public async Task Delete(long id) {

        var naturezaDeLancamento = await _naturezaDeLancamentoRepository.GetById(id);
        var usuario = _usuarioRepository.GetById(naturezaDeLancamento.IdUsuario);

        naturezaDeLancamento = await GetByIdAndIdUsuario(id, usuario.Id);

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
