using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models;

namespace DataAccess.Services
{
    public class KosaricaService : IKosaricaService
    {
        private readonly IKosaricaRepository _kosaricaRepository;

        public KosaricaService(IKosaricaRepository kosaricaRepository)
        {
            _kosaricaRepository = kosaricaRepository;
        }

        public async Task<bool> DeleteKosaricaKupacAsync(string kosaricaId)
        {
           return await _kosaricaRepository.DeleteKosaricaKupacAsync(kosaricaId);
        }

        public async Task<KosaricaKupac> GetKosaricaKupacAsync(string kosaricaId)
        {
            return await _kosaricaRepository.GetKosaricaKupacAsync(kosaricaId);
        }

        public async Task<KosaricaKupac> UpdateKosaricaKupacAsync(KosaricaKupac kosaricaKupac)
        {
            return await _kosaricaRepository.UpdateKosaricaKupacAsync(kosaricaKupac);
        }
    }
}