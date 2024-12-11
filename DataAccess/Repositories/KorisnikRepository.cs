using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models.Identity;
using Microsoft.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly IDbConnection _dbConnection;

        public KorisnikRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Korisnik> GetKorisnikByEmailAsync(string email)
        {
            const string query =
                @"SELECT  
                    k.Id,
                    k.Alias,
                    k.KorisnickoIme,
                    k.Lozinka,
                    k.Email,
                    k.AdresaID
                FROM Korisnici k 
                WHERE Email = @email";

            await using var connection = new SqlConnection(_dbConnection.ConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            using var dr = await command.ExecuteReaderAsync();
            if (await dr.ReadAsync())
            {
                return new Korisnik
                {
                    Id = dr["Id"].ToString(),
                    Alias = dr["Alias"].ToString(),
                    KorisnickoIme = dr["KorisnickoIme"].ToString(),
                    Lozinka = dr["Lozinka"].ToString(),
                    Email = dr["Email"].ToString(),
                    AdresaId = dr.GetInt32(dr.GetOrdinal("AdresaID"))
                };
            }

            return null;
        }


        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            const string query = @"SELECT 1 FROM Korisnici WHERE Email = @Email";

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                await connection.OpenAsync(); 
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        return await dr.ReadAsync();
                    }
                }
            }
        }


        public async Task<Korisnik> GetKorisnikSaAdresomAsync(string email)
        {
            const string query =
                @"SELECT  
                     k.Id,
                    k.Alias,
                    k.KorisnickoIme,
                    k.Lozinka,
                    k.Email,
                    k.AdresaID,
                    ISNULL(a.Ulica,'') AS Ulica,
                    ISNULL(a.Grad,'') AS Grad,
                    ISNULL(a.PostanskiBroj, '') AS PostanskiBroj,
                    ISNULL(a.Drzava, '') AS Drzava,
                    ISNULL(a.Ime, '') AS Ime,
                    ISNULL(a.Prezime, '') AS Prezime,
                    ISNULL(a.KorisnikId, -1) AS KorisnikId,
                    ISNULL(a.Id, -1) AS AdresaID
                FROM Korisnici k 
                    LEFT JOIN Adrese a ON k.AdresaID = a.ID
                WHERE Email = @Email";

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                await connection.OpenAsync(); 
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var dr = await command.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            return new Korisnik
                            {
                                Id = dr["Id"].ToString(),
                                Alias = dr["Alias"].ToString(),
                                KorisnickoIme = dr["KorisnickoIme"].ToString(),
                                Lozinka = dr["Lozinka"].ToString(),
                                Email = dr["Email"].ToString(),
                                AdresaId = (int)dr["AdresaID"],
                                Adresa = new Adresa
                                {
                                    Id = (int)dr["AdresaID"],
                                    Ulica = dr["Ulica"].ToString(),
                                    Grad = dr["Grad"].ToString(),
                                    PostanskiBroj = dr["PostanskiBroj"].ToString(),
                                    Drzava = dr["Drzava"].ToString(),
                                    Ime = dr["Ime"].ToString(),
                                    Prezime = dr["Prezime"].ToString(),
                                    KorisnikId = dr["KorisnikId"].ToString()
                                }
                            };
                        }
                    }
                }
            }

            return null;
        }


        public async Task<bool> CreateKorisnikAsync(Korisnik korisnik)
        {
            const string query = @"
                INSERT INTO Korisnici 
                (Id, Alias, KorisnickoIme, Lozinka, Email, AdresaID) 
                VALUES (@Id, @Alias, @KorisnickoIme, @Lozinka, @Email, @AdresaID);";

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                await connection.OpenAsync(); 
                
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", korisnik.Id);
                    command.Parameters.AddWithValue("@Alias", korisnik.Alias);
                    command.Parameters.AddWithValue("@KorisnickoIme", korisnik.KorisnickoIme);
                    command.Parameters.AddWithValue("@Lozinka", korisnik.Lozinka);
                    command.Parameters.AddWithValue("@Email", korisnik.Email);
                    command.Parameters.AddWithValue("@AdresaID", -1);

                    var affectedRows = await command.ExecuteNonQueryAsync();

                    return affectedRows > 0;
                }
            }
        }



      public async Task<bool> UpdateKorisnikAsync(Korisnik korisnik)
     {
                const string updateKorisnikQuery = @"
                UPDATE Korisnici
                SET 
                    Alias = @Alias,
                    KorisnickoIme = @KorisnickoIme,
                    Lozinka = @Lozinka,
                    Email = @Email,
                    AdresaID = @AdresaID
                WHERE Id = @Id";

            const string insertAdresaQuery = @"
                INSERT INTO Adrese (Ime, Prezime, Ulica, Grad, PostanskiBroj, Drzava, KorisnikId)
                VALUES (@Ime, @Prezime, @Ulica, @Grad, @PostanskiBroj, @Drzava, @KorisnikId);
                SELECT SCOPE_IDENTITY();";

            const string updateAdresaQuery = @"
                UPDATE Adrese
                SET 
                    Ime = @Ime,
                    Prezime = @Prezime,
                    Ulica = @Ulica,
                    Grad = @Grad,
                    PostanskiBroj = @PostanskiBroj,
                    Drzava = @Drzava
                WHERE Id = @AdresaID";

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (korisnik.AdresaId > 0 && korisnik.Adresa != null)
                        {
                            using (var adresaCommand = new SqlCommand(updateAdresaQuery, connection, transaction))
                            {
                                adresaCommand.Parameters.AddWithValue("@Ime", korisnik.Adresa.Ime);
                                adresaCommand.Parameters.AddWithValue("@Prezime", korisnik.Adresa.Prezime);
                                adresaCommand.Parameters.AddWithValue("@Ulica", korisnik.Adresa.Ulica);
                                adresaCommand.Parameters.AddWithValue("@Grad", korisnik.Adresa.Grad);
                                adresaCommand.Parameters.AddWithValue("@PostanskiBroj", korisnik.Adresa.PostanskiBroj);
                                adresaCommand.Parameters.AddWithValue("@Drzava", korisnik.Adresa.Drzava);

                                await adresaCommand.ExecuteNonQueryAsync();
                            }
                        }
                        else if (korisnik.Adresa != null)
                        {
                            using (var adresaCommand = new SqlCommand(insertAdresaQuery, connection, transaction))
                            {
                                adresaCommand.Parameters.AddWithValue("@Ime", korisnik.Adresa.Ime);
                                adresaCommand.Parameters.AddWithValue("@Prezime", korisnik.Adresa.Prezime);
                                adresaCommand.Parameters.AddWithValue("@Ulica", korisnik.Adresa.Ulica);
                                adresaCommand.Parameters.AddWithValue("@Grad", korisnik.Adresa.Grad);
                                adresaCommand.Parameters.AddWithValue("@PostanskiBroj", korisnik.Adresa.PostanskiBroj);
                                adresaCommand.Parameters.AddWithValue("@Drzava", korisnik.Adresa.Drzava);
                                adresaCommand.Parameters.AddWithValue("@KorisnikId", korisnik.Id);

                                var result = await adresaCommand.ExecuteScalarAsync();
                                if (result != null && int.TryParse(result.ToString(), out int newAdresaId))
                                {
                                    korisnik.AdresaId = newAdresaId;
                                }
                            }
                        }

                        using (var korisnikCommand = new SqlCommand(updateKorisnikQuery, connection, transaction))
                        {
                            korisnikCommand.Parameters.AddWithValue("@Id", korisnik.Id);
                            korisnikCommand.Parameters.AddWithValue("@Alias", korisnik.Alias ?? (object)DBNull.Value);
                            korisnikCommand.Parameters.AddWithValue("@KorisnickoIme", korisnik.KorisnickoIme);
                            korisnikCommand.Parameters.AddWithValue("@Lozinka", korisnik.Lozinka);
                            korisnikCommand.Parameters.AddWithValue("@Email", korisnik.Email);
                            korisnikCommand.Parameters.AddWithValue("@AdresaID", korisnik.AdresaId > 0 ? korisnik.AdresaId : -1);

                            await korisnikCommand.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> ValidirajLozinkuAsync(Korisnik korisnik, string lozinka)
        {
            //    var hashedLozinka =  await HashirajLozinkuAsync(lozinka);

            //     return korisnik.Lozinka == hashedLozinka;

            return await Task.Run(() => true);

        }

        public async Task<string> HashirajLozinkuAsync(string lozinka)
        {
            return await Task.Run(() =>
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] lozinkaBytes = Encoding.UTF8.GetBytes(lozinka);
                    byte[] hashedBytes = sha256.ComputeHash(lozinkaBytes);

                    return Convert.ToBase64String(hashedBytes);
                }
            });
        }
    }
}