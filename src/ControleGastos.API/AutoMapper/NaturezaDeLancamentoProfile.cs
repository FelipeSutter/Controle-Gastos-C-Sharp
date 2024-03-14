using AutoMapper;
using ControleGastos.API.Contracts.NaturezaDeLancamento;
using ControleGastos.API.Domain.Models;

namespace ControleGastos.API.AutoMapper;

public class NaturezaDeLancamentoProfile : Profile {

    public NaturezaDeLancamentoProfile() {

        // Dessa forma eu consigo transformar um NaturezaDeLancamento em NaturezaDeLancamentoRequestContract e vice-versa
        CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoRequestContract>().ReverseMap();
        CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoResponseContract>().ReverseMap();
    }

}
