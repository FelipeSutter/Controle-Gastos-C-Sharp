using AutoMapper;
using ControleGastos.API.Contracts.Usuario;
using ControleGastos.API.Domain.Models;
using ControleGastos.API.Domain.Repositories.Interfaces;
using ControleGastos.API.Domain.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ControleGastos.API.Domain.Services.Classes;

public class UsuarioService : IUsuarioService
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    // Colocar o private TokenService

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper) { 
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    
    }

    public async Task<UsuarioLoginResponseContract> Authenticate(UsuarioLoginRequestContract usuario)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UsuarioResponseContract>> GetAll()
    {
        var usuarios =  await _usuarioRepository.GetAll();
        return usuarios.Select(u => _mapper.Map<UsuarioResponseContract>(u));   
    }

    public async Task<UsuarioResponseContract> GetByEmail(string email)
    {
        var usuario = await _usuarioRepository.GetByEmail(email);
        return _mapper.Map<UsuarioResponseContract>(usuario);

    }

    public async Task<UsuarioResponseContract> GetById(long id)
    {
        var usuario = await _usuarioRepository.GetById(id);
        return _mapper.Map<UsuarioResponseContract>(usuario);
    }

    public async Task<UsuarioResponseContract> Add(UsuarioRequestContract request) {
        var usuario = _mapper.Map<Usuario>(request);

        usuario.Senha = GenerateHashPassword(usuario.Senha);
        usuario.DataCadastro = DateTime.Now;

        usuario = await _usuarioRepository.Add(usuario);

        return _mapper.Map<UsuarioResponseContract>(usuario);
    }

    public async Task<UsuarioResponseContract> Update(long id, UsuarioRequestContract request)
    {
        // Verificar se já existe um usuário, se não existir lança uma exception
        _ = await GetById(id) ?? throw new Exception("Usuário não encontrado para atualização");

        var usuario = _mapper.Map<Usuario>(request);
        usuario.Id = id;
        usuario.Senha = GenerateHashPassword(request.Senha);
        usuario = await _usuarioRepository.Update(usuario);

        return _mapper.Map<UsuarioResponseContract>(usuario);

    }

    public async Task Delete(long id) {
        // Acabar o método depois e continuar fazendo os outros métodos
        var usuario = await _usuarioRepository.GetById(id) ?? throw new Exception("Usuário não encontrado para inativação");
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
