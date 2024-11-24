namespace AppDomainModel.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppDomainModel.Models.Identity;

    public interface IKorisnikService
    {
        Task<Korisnik> GetKorisnikByEmailAsync(string email);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<Korisnik> GetKorisnikSaAdresomAsync(string email);
        Task<bool> CreateKorisnikAsync(Korisnik korisnik);
        Task<bool> UpdateKorisnikAsync(Korisnik korisnik);
        Task<bool> ValidirajLozinkuAsync(Korisnik korisnik, string lozinka);
        Task<string> HashirajLozinkuAsync(string lozinka);
    }   

}