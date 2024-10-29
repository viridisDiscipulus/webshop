using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDomainModel.Model;
using Microsoft.Data.SqlClient;


namespace AppDomainModel.Interfaces
{
    public interface IGernericService<T> where T : BaseModel
    {
        public Task<T> UcitajPoIdAsync(string query, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters);
        
        public Task<IEnumerable<T>> UcitajSveAsync(string query, Func<SqlDataReader, T> mapFunction);
    }
}