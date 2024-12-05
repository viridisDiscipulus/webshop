using System.Collections.Generic;
using AppDomainModel.Model;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using System;
using Microsoft.Data.SqlClient;

namespace Services {

    public class GenericService<T> : IGenericService<T> where T : BaseModel
    {
        private readonly IGenericRepository<T> _genericRepository;

        public GenericService(IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }   
        
        public async Task<T> UcitajPoIdAsync(string query, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters)
        {
             return await _genericRepository.UcitajPoIdAsync(query, mapFunction, parameters);
        }

        public async Task<IEnumerable<T>> UcitajSveAsync(string query, Func<SqlDataReader, T> mapFunction)
        {
             return await _genericRepository.UcitajSveAsync(query, mapFunction);
        }

        public async Task<int> DodajAsync(string query, params SqlParameter[] parameters)
        {
             return await _genericRepository.DodajAsync(query, parameters);
        }

        public async Task<int> AzurirajAsync(string query, params SqlParameter[] parameters)
        {
             return await _genericRepository.AzurirajAsync(query, parameters);
        }

        public async Task<int> ObrisiAsync(string query, params SqlParameter[] parameters)
        {
             return await _genericRepository.ObrisiAsync(query, parameters);
        }
        
    }
}
