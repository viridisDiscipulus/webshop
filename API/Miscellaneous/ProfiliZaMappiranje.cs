using API.Controllers;
using API.DTOs;
using AppDomainModel.Model;
using AppDomainModel.Models;
using AppDomainModel.Models.Identity;
using AppDomainModel.Models.NarudzbeSkupno;
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

        CreateMap<AppDomainModel.Models.Identity.Adresa, AdresaPovratniModel>().ReverseMap();
        CreateMap<KosaricaArtiklPovratniModel, KosaricaArtikl>().ReverseMap();
        CreateMap<KosaricaKupacPovratnoModel, KosaricaKupac>().ReverseMap();

        CreateMap<Narudzba, NarudzbaPovratPovratniModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.NacinIsporuke, o => o.MapFrom(s => s.NacinIsporuke.KratkiNaziv))
            .ForMember(d => d.CijenaDostave, o => o.MapFrom(s => s.NacinIsporuke.Cijena))
            .ForMember(d => d.SveukupnaCijena, o => o.MapFrom(s => s.PunaCijena()));

        CreateMap<NaruceniArtikl, NaruceniArtikliPovratniModel>()
            .ForMember(d => d.ProizvodId, o => o.MapFrom(s => s.ArtiklNaruceni.ProizvodArtiklId))
            .ForMember(d => d.ProizvodNaziv, o => o.MapFrom(s => s.ArtiklNaruceni.ProizvodNaziv))
            .ForMember(d => d.SlikaUrl, o => o.MapFrom(s => s.ArtiklNaruceni.SlikaUrl))
            .ForMember(d => d.SlikaUrl, o => o.MapFrom<NaruceniArtiklUrlUpravitelj>());

        CreateMap<AdresaPovratniModel, AppDomainModel.Models.NarudzbeSkupno.Adresa>();
      }
    }
}