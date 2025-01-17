using System.Threading.Tasks;
using AppDomainModel.Models;

namespace AppDomainModel.Interfaces
{
    public interface IKosaricaRepository
    {
        Task<KosaricaKupac> GetKosaricaKupacAsync(string kosaricaId);
        Task<KosaricaKupac> UpdateKosaricaKupacAsync(KosaricaKupac kosaricaKupac);
        Task<bool> DeleteKosaricaKupacAsync(string kosaricaId);
    }
}