
using System;
using System.Text.Json;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models;
using StackExchange.Redis;

namespace DataAccess.Repositories
{
    public class KosaricaRepository : IKosaricaRepository
    {
        private readonly IDatabase _database;

        public KosaricaRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteKosaricaKupacAsync(string kosaricaId)
        {
            return await _database.KeyDeleteAsync(kosaricaId);
        }

        public async Task<KosaricaKupac> GetKosaricaKupacAsync(string kosaricaId)
        {
            var data = await _database.StringGetAsync(kosaricaId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<KosaricaKupac>(data); 
        }

        public async Task<KosaricaKupac> UpdateKosaricaKupacAsync(KosaricaKupac kosaricaKupac)
        {
            var newData = await _database.StringSetAsync(kosaricaKupac.Id, 
                JsonSerializer.Serialize(kosaricaKupac), TimeSpan.FromDays(30));

                if(!newData) return null;

                return await GetKosaricaKupacAsync(kosaricaKupac.Id);
        }
    }

}