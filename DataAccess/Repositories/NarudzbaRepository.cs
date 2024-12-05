using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models.NarudzbeSkupno;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class NarudzbaRepository : INarudzbaRepository
    {
        private readonly IDbConnection _dbConnection;

        public NarudzbaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Narudzba> CreateNarudzbuAsync(string kupacEmail, int nacinIsporukeId, string kosaricaId, Adresa adresaDostave)
        {
            return await Task.Run(() => new Narudzba());
        }

        public async Task<IReadOnlyList<NacinIsporuke>> GetNacineIsporukeAsync()
        {
            return await Task.Run(() => new List<NacinIsporuke>());
        }

        public async Task<Narudzba> GetNarudzbaByIdAsync(int id)
        {
            return await Task.Run(() => new Narudzba());
        }

        public async Task<IReadOnlyList<Narudzba>> GetNarudzbeKupcaAsync(string kupacEmail)
        {
            return await Task.Run(() => new List<Narudzba>());
        }
    }
}