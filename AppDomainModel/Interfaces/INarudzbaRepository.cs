using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Models.NarudzbeSkupno;

namespace AppDomainModel.Interfaces
{
    public interface INarudzbaRepository
    {
        Task<Narudzba> CreateNarudzbuAsync(string kupacEmail, int nacinIsporuke, string kosaricaId, Adresa adresaDostave);
        Task<IReadOnlyList<Narudzba>> GetNarudzbeKupcaAsync(string kupacEmail);
        Task<Narudzba> GetNarudzbaByIdAsync(int id);
        Task<IReadOnlyList<NacinIsporuke>> GetNacineIsporukeAsync();
    }
}