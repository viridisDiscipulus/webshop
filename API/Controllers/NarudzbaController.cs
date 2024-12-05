using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.ErrorTypes;
using AppDomainModel.Interfaces;
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
        private readonly IMapper _mapper;

        public NarudzbaController(INarudzbaService narudzbaService, IMapper mapper)
        {
            _narudzbaService = narudzbaService;
            _mapper = mapper;
        }

         [HttpPost]
        public async Task<ActionResult<Narudzba>> KreirajNarudzbu(NarudzbaPovratniModel narudzbaPovratniModel)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var adresa = _mapper.Map<AdresaPovratniModel, Adresa>(narudzbaPovratniModel.AdresaDostave);

            var narudzba = await _narudzbaService.CreateNarudzbuAsync(email, narudzbaPovratniModel.NacinIsporukeId, narudzbaPovratniModel.KosaricaId, adresa);

            if (narudzba == null) return BadRequest(new ApiResponse(400));

            return Ok(narudzba);
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
        public async Task<ActionResult<NarudzbaPovratniModel>> GetOrderByIdForUser(int id)
        {
           var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var narudzba = await _narudzbaService.GetNarudzbaByIdAsync(id, email);

            narudzba = await _narudzbaService.UcitajArtikleNarudzbe(narudzba);

            if (narudzba == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Narudzba, NarudzbaPovratniModel>(narudzba));
        }

        [HttpGet("naciniIsporuke")]
        public async Task<ActionResult<IReadOnlyList<NacinIsporuke>>> GetDeliveryMethods()
        {
            return Ok(await _narudzbaService.GetNacineIsporukeAsync());
        }
        
    }
}