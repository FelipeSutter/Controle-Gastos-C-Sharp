using AutoMapper;
using ControleGastos.API.Contracts.Usuario;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;
using ControleGastos.API.Exceptions;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace ControleGastos.API.Domain.Services.Classes;

public class UsuarioService : IUsuarioService
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, TokenService tokenService) { 
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    
    }

    public async Task<UsuarioLoginResponseContract> Authenticate(UsuarioLoginRequestContract contract)
    {
        // Pega o email e a senha do usuario e verifica se existe esse email e se a senha é a correta
        // Caso seja verdadeiro retorna um response passando o id, email e o token para autenticação
        var usuario = await GetByEmail(contract.Email);

        var hashPassword = GenerateHashPassword(contract.Senha);

        if (usuario == null || usuario.Senha != hashPassword) {
            throw new UnauthorizedException("Usuário ou Senha inválidos");
        }

        return new UsuarioLoginResponseContract {
            Id = usuario.Id,
            Email = usuario.Email,
            Token = _tokenService.GenerateToken(_mapper.Map<Usuario>(usuario)),
        };

    }

    public async Task<IEnumerable<UsuarioResponseContract>> GetAll(long idUsuario)
    {
        var usuarios =  await _usuarioRepository.GetAll();
        return usuarios.Select(u => _mapper.Map<UsuarioResponseContract>(u));   
    }

    public async Task<UsuarioResponseContract> GetByEmail(string email)
    {
        var usuario = await _usuarioRepository.GetByEmail(email);
        return _mapper.Map<UsuarioResponseContract>(usuario);

    }

    public async Task<UsuarioResponseContract> GetById(long id, long idUsuario)
    {
        var usuario = await _usuarioRepository.GetById(id);
        return _mapper.Map<UsuarioResponseContract>(usuario);
    }

    public async Task<UsuarioResponseContract> Add(UsuarioRequestContract request, long idUsuario) {
        var usuario = _mapper.Map<Usuario>(request);

        usuario.Senha = GenerateHashPassword(usuario.Senha);
        usuario.DataCadastro = DateTime.Now;

        usuario = await _usuarioRepository.Add(usuario);

        return _mapper.Map<UsuarioResponseContract>(usuario);
    }

    public async Task<UsuarioResponseContract> Update(long id, UsuarioRequestContract request, long idUsuario)
    {
        // Verificar se já existe um usuário, se não existir lança uma exception
        _ = await GetById(id, 0) ?? throw new NotFoundException("Usuário não encontrado para atualização");

        var usuario = _mapper.Map<Usuario>(request);
        usuario.Id = id;
        usuario.Senha = GenerateHashPassword(request.Senha);
        usuario = await _usuarioRepository.Update(usuario);

        return _mapper.Map<UsuarioResponseContract>(usuario);

    }

    public async Task Delete(long id, long idUsuario) {
        // Acabar o método depois e continuar fazendo os outros métodos
        var usuario = await _usuarioRepository.GetById(id) ?? throw new NotFoundException("Usuário não encontrado para inativação");
        await _usuarioRepository.Delete(_mapper.Map<Usuario>(usuario));
    }


    private string GenerateHashPassword(string senha)
    {
        string hashPassword;
        // Basicamente criptografa a senha utilizando o SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(senha);
            byte[] byteHashPassword = sha256.ComputeHash(bytePassword);
            hashPassword = BitConverter.ToString(byteHashPassword).Replace("-", "").ToLower();
        }

        return hashPassword;



    }
}
