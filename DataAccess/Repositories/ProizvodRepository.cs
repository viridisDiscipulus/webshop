using System.Collections.Generic;
using System.Threading.Tasks;
using AppDomainModel.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly string _connectionString;

        public ProizvodRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Proizvod> UcitajZaProizvodID(int id)
        {
            Proizvod proizvod = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  

                SqlCommand cmd = new SqlCommand("SELECT * FROM Proizvod WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        proizvod = new Proizvod
                        {
                            Id = (int)dr["ID"],
                            Naziv = dr["Naziv"].ToString(),
                        };
                    }
                }
            }

            return proizvod;
        }

       public async Task<IEnumerable<Proizvod>> UcitajSveProizvode()
        {
            List<Proizvod> proizvodi = new List<Proizvod>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  

                SqlCommand cmd = new SqlCommand("SELECT Id, Naziv FROM Proizvod", con);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync()) 
                {
                    while (await dr.ReadAsync())  
                    {
                        proizvodi.Add(new Proizvod
                        {
                            Id = (int)dr["Id"],
                            Naziv = (string)dr["Naziv"]
                        });
                    }
                }
            }

            return proizvodi;
        }

        public void SnimiProizvod(Proizvod proizvod)
        {
             using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Proizvodi (Naziv) VALUES (@Naziv)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Naziv", proizvod.Naziv);
                
                command.ExecuteNonQuery();
            }
        }

        public void AzurirajProizvod(Proizvod proizvod)
        {
           using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Proizvodi SET Naziv = @Naziv WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", proizvod.Id);
                command.Parameters.AddWithValue("@Naziv", proizvod.Naziv);

                command.ExecuteNonQuery();
            }
        }

        public void ObrisiProizvod(int proizvodID)
        {
              using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Proizvodi WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", proizvodID);

                command.ExecuteNonQuery();
            }
        }
    }
}