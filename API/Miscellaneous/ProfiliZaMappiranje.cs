using AppDomainModel.Model;
using AutoMapper;

namespace API.Miscellaneous
{
    public class ProfiliZaMappiranje : Profile
    {
      public ProfiliZaMappiranje()
      {
        CreateMap<Proizvod, DTOs.ProizvodPovratniModel>()
            .ForMember(d => d.VrstaProizvoda, o => o.MapFrom(s => s.VrstaProizvoda.Naziv))
            .ForMember(d => d.RobnaMarka, o => o.MapFrom(s => s.RobnaMarka.Naziv))
            .ForMember(d => d.SlikaUrl, o => o.MapFrom<URLUpravitelj>());
      }
    }
}