using System.Collections.Generic;
using AppDomainModel.Model;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using System;
using Microsoft.Data.SqlClient;

namespace Services {

    public class GenericService<T> : IGernericService<T> where T : BaseModel
    {
        private readonly IGernericRepository<T> _genericRepository;

        public GenericService(IGernericRepository<T> genericRepository)
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
    }
}
