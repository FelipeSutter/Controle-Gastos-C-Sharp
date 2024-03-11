using AutoMapper;
using ControleGastos.API.Contracts.Usuario;
using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.AutoMapper;

public class UsuarioProfile : Profile {

    public UsuarioProfile() {

        // Dessa forma eu consigo transformar um Usuario em UsuarioRequestContract e vice-versa
        CreateMap<Usuario, UsuarioRequestContract>().ReverseMap();
        CreateMap<Usuario, UsuarioResponseContract>().ReverseMap();
    }

}
