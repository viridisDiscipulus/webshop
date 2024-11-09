using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.DTOs;
using API.ErrorTypes;
using AppDomainModel.Interfaces;
using AppDomainModel.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace API.Controllers
{   
    public class ProizvodController : BaseApiController
    {
        private readonly IGernericService<Proizvod> _proizvodService;
        private readonly IGernericService<RobnaMarka> _robnaMarkaService;
        private readonly IGernericService<VrstaProizvoda> _vrstaProizvodaService;
        private readonly IMapper _mapper;

        public ProizvodController(IGernericService<Proizvod> proizvodService, IGernericService<RobnaMarka> robnaMarkaService, IGernericService<VrstaProizvoda> vrstaProizvodaService, IMapper mapper)
        {
            _mapper = mapper;
            _proizvodService = proizvodService;
            _robnaMarkaService = robnaMarkaService;
            _vrstaProizvodaService = vrstaProizvodaService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]  
        public async Task<ActionResult<ProizvodPovratniModel>> UcitajProizvod(int id)
        {
            string query = @"
                SELECT 
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

            var proizvod = await _proizvodService.UcitajPoIdAsync(query, dr => new Proizvod
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
            }, new SqlParameter("@ID", id));

            if (proizvod == null)
            {
                return NotFound(new ApiResponse(404)); 
            }

            var proizvodPovratniModel = _mapper.Map<Proizvod, ProizvodPovratniModel>(proizvod);

            return Ok(proizvodPovratniModel);
        }

        [HttpGet("SviProizvodi")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ProizvodPovratniModel>>> UcitajSveProizvode(
           string sortiranje = "Naziv",           
           int? robnaMarkaID = null,          
           int? vrstaProizvodaID = null,
           string pretraga = null)
      
        {
           // Osnovni upit
            var query = new StringBuilder(@"
                SELECT 
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
                WHERE 1=1"); 

            if (robnaMarkaID.HasValue)
            {
                query.Append($" AND p.RobnaMarkaID = {robnaMarkaID}");
            }
            if (vrstaProizvodaID.HasValue)
            {
                query.Append($" AND p.VrstaProizvodaID = {vrstaProizvodaID}");
            }
            if (!string.IsNullOrEmpty(pretraga))
            {
                query.Append($" AND p.Naziv LIKE '%{pretraga}%'");
            }


            query.Append($" ORDER BY {sortiranje}");

            // Izvrsavanje upit s parametrima za filtriranje i oznacavanje stranica
            var proizvodi = await _proizvodService.UcitajSveAsync(query.ToString(), dr => new Proizvod
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

            if (proizvodi == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<Proizvod>, List<ProizvodPovratniModel>>((List<Proizvod>)proizvodi));
        }

        // [HttpPost]
        // public IActionResult SnimiProizvod([FromBody] Proizvod proizvod)
        // {
        //     if (proizvod == null)
        //         return BadRequest();

        //     _proizvodService.SnimiProizvod(proizvod);
        //     return Ok();
        // }

        // [HttpPut("{id}")]
        // public IActionResult AzurirajProizvod(int id, [FromBody] Proizvod proizvod)
        // {
        //     proizvod.Id = id;
        //     _proizvodService.AzurirajProizvod(proizvod);
        //     return Ok();
        // }

        // [HttpDelete("{id}")]
        // public IActionResult ObrisiProizvod(int id)
        // {
        //     _proizvodService.ObrisiProizvod(id);
        //     return Ok();
        // }

        [HttpGet("robneMarke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]  
        public async Task<ActionResult<List<RobnaMarka>>> UcitajSveRobneMarke()
        {
             string query = @"
                SELECT 
                    Id,
                    Naziv
                FROM 
                    RobnaMarka;";

            var robnaMarka = await _robnaMarkaService.UcitajSveAsync(query, dr => new RobnaMarka
            {
                Id = (int)dr["Id"],
                Naziv = (string)dr["Naziv"]
            });

            if (robnaMarka == null)
            {
                return NotFound();
            }

            return Ok(robnaMarka);
        }

        [HttpGet("vrsteProizvoda")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]  
        public async Task<ActionResult<List<VrstaProizvoda>>> UcitajSveVrsteProizvoda()
        {
           string query = @"
                SELECT 
                    Id,
                    Naziv
                FROM 
                    VrstaProizvoda;";

            var vrstaProizvoda = await _vrstaProizvodaService.UcitajSveAsync(query, dr => new VrstaProizvoda
            {
                Id = (int)dr["Id"],
                Naziv = (string)dr["Naziv"]
            });

            if (vrstaProizvoda == null)
            {
                return NotFound();
            }

            return Ok(vrstaProizvoda);
        }
    }
}