using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Interfaces
{
    public interface IKosaricaRepository
    {
        Task<Kosarica> GetKosaricaAsync(string kosaricaId);
        Task<Kosarica> UpdateKosaricaAsync(Kosarica kosarica);
        Task<bool> DeleteKosaricaAsync(string kosaricaId);
    }
}