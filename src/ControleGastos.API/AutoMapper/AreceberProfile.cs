using AutoMapper;
using ControleGastos.API.Contracts.Areceber;
using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.AutoMapper;

public class AreceberProfile : Profile {

    public AreceberProfile() {

        // Dessa forma eu consigo transformar um Areceber em AreceberRequestContract e vice-versa
        CreateMap<Areceber, AreceberRequestContract>().ReverseMap();
        CreateMap<Areceber, AreceberResponseContract>().ReverseMap();
    }

}
