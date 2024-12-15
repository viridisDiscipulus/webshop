using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.ErrorTypes;
using API.Miscellaneous;
using AppDomainModel.Interfaces;
using AppDomainModel.Models;
using AppDomainModel.Models.NarudzbeSkupno;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Authorize]
    public class NarudzbaController : BaseApiController
    {
        private readonly INarudzbaService _narudzbaService;
        private readonly INaplataService _naplataService;
        private readonly IMapper _mapper;

        public NarudzbaController(INarudzbaService narudzbaService, INaplataService naplataService, IMapper mapper)
        {
            _narudzbaService = narudzbaService;
            _naplataService = naplataService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Narudzba>> KreirajNarudzbu(NarudzbaPovratniModel narudzbaPovratniModel)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var adresa = _mapper.Map<AdresaPovratniModel, Adresa>(narudzbaPovratniModel.AdresaDostave);

            var narudzba = await _narudzbaService.CreateNarudzbuAsync(email, narudzbaPovratniModel.NacinIsporukeId, narudzbaPovratniModel.KosaricaId, adresa);

            if (narudzba == null) return BadRequest(new ApiResponse(400, "Problem kod kreiranje narudžbe"));

            var narudzbaDto = new NarudzbaPovratPovratniModel
            {
                Id = narudzba.Id,
                KupacEmail = narudzba.KupacEmail,
                DatumNarudzbe = narudzba.DatumNarudzbe.ToString("o"),
                AdresaDostave = narudzba.AdresaDostave,
                NacinIsporuke = narudzba.NacinIsporuke.KratkiNaziv,
                CijenaDostave = narudzba.NacinIsporuke.Cijena,
                NaruceniArtikli = _mapper.Map<IReadOnlyList<NaruceniArtikl>, IReadOnlyList<NaruceniArtikliPovratniModel>>(narudzba.NaruceniArtikli),
                UkupnaCijena = narudzba.UkupnaCijena,
                Status = EnumExtensions.GetEnumMemberValue(narudzba.Status),
                SveukupnaCijena = narudzba.PunaCijena()
            };

            return Ok(narudzbaDto);
        }

        [HttpGet]
        public async Task<ActionResult<List<NarudzbaPovratPovratniModel>>> GetNarudzbeKupaca()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var narudzbe = await _narudzbaService.GetNarudzbeKupcaAsync(email);

            narudzbe = await _narudzbaService.UcitajArtikleNarudzbi(narudzbe);

            return Ok(_mapper.Map<List<Narudzba>, List<NarudzbaPovratPovratniModel>>(narudzbe));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NarudzbaPovratPovratniModel>> GetNarudzbaByIdAsync(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var narudzba = await _narudzbaService.GetNarudzbaByIdAsync(id, email);

            narudzba = await _narudzbaService.UcitajArtikleNarudzbe(narudzba);

            if (narudzba == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Narudzba, NarudzbaPovratPovratniModel>(narudzba));
        }

        [HttpGet("nacinIsporuke")]
        public async Task<ActionResult<IReadOnlyList<NacinIsporuke>>> GetNacineIsporukeAsync()
        {
            return Ok(await _narudzbaService.GetNacineIsporukeAsync());
        }
    
       [HttpGet("placanje")]
        public async Task<IActionResult> ProvjeriPodatkePlacanja([FromQuery] Placanje placanjeRequest)
        {
            var preduvjetiZaPlacanje = await Task.Run(() => _naplataService.ProvediTransakciju(placanjeRequest));

            if (preduvjetiZaPlacanje == false)
            {
                return  Unauthorized(new ApiResponse(401, "Neispravni podaci kartice."));
            }

            return Ok(true);
        }


    }
}