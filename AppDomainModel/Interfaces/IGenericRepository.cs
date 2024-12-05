using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDomainModel.Model;
using Microsoft.Data.SqlClient;


namespace AppDomainModel.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        public Task<T> UcitajPoIdAsync(string query, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters);
        public Task<IEnumerable<T>> UcitajSveAsync(string query, Func<SqlDataReader, T> mapFunction);
        public Task<int> DodajAsync(string query, params SqlParameter[] parameters);
        public Task<int> AzurirajAsync(string query, params SqlParameter[] parameters);
        public Task<int> ObrisiAsync(string query, params SqlParameter[] parameters);
    }
}