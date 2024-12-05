using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AppDomainModel.Models.NarudzbeSkupno;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace API.Miscellaneous
{
    public class NaruceniArtiklUrlUpravitelj: IValueResolver<NaruceniArtikl, NaruceniArtikliPovratniModel, string>
    {
        private IConfiguration _konfiguracija;

        public NaruceniArtiklUrlUpravitelj(IConfiguration konfiguracija)
        {
            _konfiguracija = konfiguracija;
        }

        public string Resolve(NaruceniArtikl source, NaruceniArtikliPovratniModel destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ArtiklNaruceni.SlikaUrl))
            {
                return _konfiguracija["ApiUrl"] + source.ArtiklNaruceni.SlikaUrl;
            }

            return null;
        }
    }
}