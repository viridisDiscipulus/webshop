using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Models;

namespace AppDomainModel.Interfaces
{
    public interface IKosaricaService
    {
        Task<KosaricaKupac> GetKosaricaKupacAsync(string kosaricaId);
        Task<KosaricaKupac> UpdateKosaricaKupacAsync(KosaricaKupac kosaricaKupac);
        Task<bool> DeleteKosaricaKupacAsync(string kosaricaId);
    }
}