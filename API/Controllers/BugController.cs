using System.Data;
using API.ErrorTypes;
using AppDomainModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    public class BugController : BaseApiController
    {
       private readonly string _connectionString;

        public BugController(IDbConnection connection)
        {
            _connectionString = connection.ConnectionString;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }
        
        [HttpGet("notfound")]
        public ActionResult GetNotFoundError()
        {
            var proizvod = GetProizvodById(100);

            if (proizvod == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(proizvod);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var proizvod = GetProizvodById(100);

            var proizvodString = proizvod.ToString();

            return Ok(proizvodString);
        }
 
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Proizvod GetProizvodById(int id)
        { 
           Proizvod proizvod = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();  

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
                cmd.Parameters.AddWithValue("@ID", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
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
    }
}