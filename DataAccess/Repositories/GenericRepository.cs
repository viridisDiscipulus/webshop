using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Model;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGernericRepository<T> where T : BaseModel
    {
        private readonly string _connectionString;

        public GenericRepository(IDbConnection connection)
        {
            _connectionString = connection.ConnectionString;
        }

        public async Task<T> UcitajPoIdAsync(string query, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters)
        {
             T item = default;

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddRange(parameters);

            using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
            {
                if (await dr.ReadAsync())
                {
                    item = mapFunction(dr);
                }
            }
         }

            return item;
        }

       public async Task<IEnumerable<T>> UcitajSveAsync(string query, Func<SqlDataReader, T> mapFunction)
        {
            List<T> items = new List<T>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        items.Add(mapFunction(dr));
                    }
                }
            }

            return items;
        }
    }
 
}