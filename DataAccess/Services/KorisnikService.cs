using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models.Identity;

namespace DataAccess.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository _korisnikRepository;

        public KorisnikService(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }

        public Task<bool> CheckEmailExistsAsync(string email)
        {
            return _korisnikRepository.CheckEmailExistsAsync(email);
        }

        public Task<bool> CreateKorisnikAsync(Korisnik korisnik)
        {
            return _korisnikRepository.CreateKorisnikAsync(korisnik);
        }

        public Task<Korisnik> GetKorisnikByEmailAsync(string email, string lozinka)
        {
            return _korisnikRepository.GetKorisnikByEmailAsync(email, lozinka);
        }

        public Task<Korisnik> GetKorisnikSaAdresomAsync(string email)
        {
            return _korisnikRepository.GetKorisnikSaAdresomAsync(email);
        }

        public Task<bool> UpdateKorisnikAsync(Korisnik korisnik)
        {
           return _korisnikRepository.UpdateKorisnikAsync(korisnik);
        }
    }
}