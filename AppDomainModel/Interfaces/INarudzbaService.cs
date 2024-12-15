using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Models.NarudzbeSkupno;

namespace AppDomainModel.Interfaces
{
    public interface INarudzbaService
    {
        Task<Narudzba> CreateNarudzbuAsync(string kupacEmail, int nacinIsporuke, string kosaricaId, Adresa adresaDostave);
        Task<List<Narudzba>> GetNarudzbeKupcaAsync(string kupacEmail);
        Task<Narudzba> GetNarudzbaByIdAsync(int id, string email);
        Task<IReadOnlyList<NacinIsporuke>> GetNacineIsporukeAsync();
        Task<Narudzba> UcitajArtikleNarudzbe(Narudzba narudzba);
        Task<List<Narudzba>> UcitajArtikleNarudzbi(List<Narudzba> narudzbe);
        Task<List<Narudzba>> GetNarudzbeKupcaZaAdministratoraAsync();


    }
}