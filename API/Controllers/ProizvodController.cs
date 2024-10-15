using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ProizvodController : ControllerBase
    {
        private readonly IProizvodService _proizvodService;

        public ProizvodController(IProizvodService proizvodService)
        {
            _proizvodService = proizvodService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proizvod>> UcitajProizvod(int id)
        {
            var proizvod = await _proizvodService.UcitajZaProizvodID(id);
            if (proizvod == null)
                return NotFound();

            return Ok(proizvod);
        }

        [HttpGet]
        public async Task<ActionResult<List<Proizvod>>> UcitajSveProizvode()
        {
            var proizvodi = await _proizvodService.UcitajSveProizvode();
            return Ok(proizvodi);
        }

        [HttpPost]
        public IActionResult SnimiProizvod([FromBody] Proizvod proizvod)
        {
            if (proizvod == null)
                return BadRequest();

            _proizvodService.SnimiProizvod(proizvod);
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult AzurirajProizvod(int id, [FromBody] Proizvod proizvod)
        {
            proizvod.Id = id;
            _proizvodService.AzurirajProizvod(proizvod);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ObrisiProizvod(int id)
        {
            _proizvodService.ObrisiProizvod(id);
            return Ok();
        }


    }
}