using API.DTOs;
using AppDomainModel.Model;
using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace API.Miscellaneous
{
    public class URLUpravitelj : IValueResolver<Proizvod, ProizvodPovratniModel, string>
    {
        private readonly IConfiguration _configuration;
        public URLUpravitelj(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        public string Resolve(Proizvod source, ProizvodPovratniModel destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.SlikaUrl))
            {
                return _configuration["ApiUrl"] + source.SlikaUrl;
            }

            return null;
        }
    }
}