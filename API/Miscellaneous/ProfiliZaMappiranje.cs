using API.DTOs;
using AppDomainModel.Model;
using AppDomainModel.Models;
using AppDomainModel.Models.Identity;
using AutoMapper;

namespace API.Miscellaneous
{
    public class ProfiliZaMappiranje : Profile
    {
      public ProfiliZaMappiranje()
      {
        CreateMap<Proizvod, ProizvodPovratniModel>()
            .ForMember(d => d.VrstaProizvoda, o => o.MapFrom(s => s.VrstaProizvoda.Naziv))
            .ForMember(d => d.RobnaMarka, o => o.MapFrom(s => s.RobnaMarka.Naziv))
            .ForMember(d => d.SlikaUrl, o => o.MapFrom<URLUpravitelj>());

        CreateMap<Adresa, AdresaPovratniModel>();
        CreateMap<AdresaPovratniModel, Adresa>();
        CreateMap<KosaricaArtikl, KosaricaArtiklPovratniModel>();
        CreateMap<KosaricaArtiklPovratniModel, KosaricaArtikl>();
        CreateMap<KosaricaKupac, KosaricaKupacPovratnoModel>();
        CreateMap<KosaricaKupacPovratnoModel, KosaricaKupac>();
      }
    }
}