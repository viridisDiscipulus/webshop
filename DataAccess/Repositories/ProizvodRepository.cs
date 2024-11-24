using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Model;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class ProizvodRepository : IProizvodRepository
    {
        private readonly string _connectionString;

        public ProizvodRepository(IDbConnection connection)
        {
            _connectionString = connection.ConnectionString;
        }

        public async Task<Proizvod> UcitajZaProizvodIdAsync(int Id)
        {
            Proizvod proizvod = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  

                string query = @"SELECT 
                    p.Id,
                    p.Naziv,
                    p.Opis,
                    p.Cijena,
                    p.SlikaUrl,
                    p.VrstaProizvodaID,
                    vp.Naziv AS VrstaProizvodaNaziv,
                    p.RobnaMarkaID,
                    rm.Naziv AS RobnaMarkaNaziv
                FROM 
                    Proizvod p
                INNER JOIN 
                    VrstaProizvoda vp ON p.VrstaProizvodaID = vp.ID
                INNER JOIN 
                    RobnaMarka rm ON p.RobnaMarkaID = rm.ID
                WHERE 
                    p.Id = @ID;";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", Id);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        proizvod = new Proizvod
                        {
                            Id = (int)dr["Id"],
                            Naziv = (string)dr["Naziv"],
                            Opis = (string)dr["Opis"],
                            Cijena = (decimal)dr["Cijena"],
                            SlikaUrl = (string)dr["SlikaUrl"],
                            VrstaProizvodaID = (int)dr["VrstaProizvodaID"],
                            VrstaProizvoda = new VrstaProizvoda
                            {
                                Id = (int)dr["VrstaProizvodaID"],
                                Naziv = (string)dr["VrstaProizvodaNaziv"]
                            },
                            RobnaMarkaID = (int)dr["RobnaMarkaID"],
                            RobnaMarka = new RobnaMarka
                            {
                                Id = (int)dr["RobnaMarkaID"],
                                Naziv = (string)dr["RobnaMarkaNaziv"]
                            }
                        };
                    }
                }
            }

            return proizvod;
        }

        public async Task<IEnumerable<Proizvod>> UcitajSveProizvodeAsync()
        {
            List<Proizvod> proizvodi = new List<Proizvod>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  

                 string query = @"SELECT 
                    p.Id,
                    p.Naziv,
                    p.Opis,
                    p.Cijena,
                    p.SlikaUrl,
                    p.VrstaProizvodaID,
                    vp.Naziv AS VrstaProizvodaNaziv,
                    p.RobnaMarkaID,
                    rm.Naziv AS RobnaMarkaNaziv
                FROM 
                    Proizvod p
                INNER JOIN 
                    VrstaProizvoda vp ON p.VrstaProizvodaID = vp.ID
                INNER JOIN 
                    RobnaMarka rm ON p.RobnaMarkaID = rm.ID;";

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync()) 
                {
                    while (await dr.ReadAsync())  
                    {
                        proizvodi.Add(new Proizvod
                        {
                            Id = (int)dr["Id"],
                            Naziv = (string)dr["Naziv"],
                            Opis = (string)dr["Opis"],
                            Cijena = (decimal)dr["Cijena"],
                            SlikaUrl = (string)dr["SlikaUrl"],
                            VrstaProizvodaID = (int)dr["VrstaProizvodaID"],
                            VrstaProizvoda = new VrstaProizvoda
                            {
                                Id = (int)dr["VrstaProizvodaID"],
                                Naziv = (string)dr["VrstaProizvodaNaziv"]
                            },
                            RobnaMarkaID = (int)dr["RobnaMarkaID"],
                            RobnaMarka = new RobnaMarka
                            {
                                Id = (int)dr["RobnaMarkaID"],
                                Naziv = (string)dr["RobnaMarkaNaziv"]
                            }
                        });
                    }
                }
            }

            return proizvodi;
        }

        public async Task<IEnumerable<RobnaMarka>> UcitajSveRobneMarkeAsync()
        {
            List<RobnaMarka> robnaMarka = new List<RobnaMarka>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  

                 string query = @"SELECT ID
                                        ,Naziv
                                FROM dbo.RobnaMarka;";

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync()) 
                {
                    while (await dr.ReadAsync())  
                    {
                        robnaMarka.Add(new RobnaMarka
                        {
                            Id = (int)dr["Id"],
                            Naziv = (string)dr["Naziv"],
                           
                        });
                    }
                }
            }

            return robnaMarka;
        }

        public async Task<IEnumerable<VrstaProizvoda>> UcitajSveVrsteProizvodaAsync()
        {
            List<VrstaProizvoda> vrstaProizvoda = new List<VrstaProizvoda>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();  

                 string query = @"SELECT ID
                                        ,Naziv
                                FROM dbo.VrstaProizvoda;";

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync()) 
                {
                    while (await dr.ReadAsync())  
                    {
                        vrstaProizvoda.Add(new VrstaProizvoda
                        {
                            Id = (int)dr["Id"],
                            Naziv = (string)dr["Naziv"],
                           
                        });
                    }
                }
            }

            return vrstaProizvoda;
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

                string query = "UPDATE Proizvodi SET Naziv = @Naziv WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ID", proizvod.Id);
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

                command.Parameters.AddWithValue("@ID", proizvodID);

                command.ExecuteNonQuery();
            }
        }

    }
}