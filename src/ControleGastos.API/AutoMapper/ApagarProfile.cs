using AutoMapper;
using ControleGastos.API.Contracts.Apagar;
using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.AutoMapper;

public class ApagarProfile : Profile {

    public ApagarProfile() {

        // Dessa forma eu consigo transformar um Apagar em ApagarRequestContract e vice-versa
        CreateMap<Apagar, ApagarRequestContract>().ReverseMap();
        CreateMap<Apagar, ApagarResponseContract>().ReverseMap();
    }

}
