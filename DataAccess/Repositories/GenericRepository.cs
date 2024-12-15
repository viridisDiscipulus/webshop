using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Model;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly string _connectionString;

        public GenericRepository(IDbConnection connection)
        {
            _connectionString = connection.ConnectionString;
        }

        public async Task<T> UcitajPoIdAsync(string query, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters)
        {
            T item = default;

            try
            {
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
            }
            catch (SqlException sqlEx)
            {
                Console.Error.WriteLine($"SQL Error: {sqlEx.Message}\nStack Trace: {sqlEx.StackTrace}");
                throw new Exception("Došlo je do pogreške u bazi podataka tijekom izvođenja upita.", sqlEx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                throw new Exception("Došlo je do neočekivane pogreške tijekom izvođenja upita.", ex);
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

        public async Task<int> DodajAsync(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                // Vraca ID novog zapisa
                query += "; SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddRange(parameters);
                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task<int> AzurirajAsync(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
            await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddRange(parameters);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> ObrisiAsync(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
            await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddRange(parameters);
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
 
}