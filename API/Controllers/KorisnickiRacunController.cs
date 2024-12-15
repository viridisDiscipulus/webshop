using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.ErrorTypes;
using AppDomainModel.Interfaces;
using AppDomainModel.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class KorisnickiRacunController : BaseApiController
    {
        private readonly IKorisnikService _korisnikService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public KorisnickiRacunController(IKorisnikService korisnikService, ITokenService tokenService, IMapper mapper)
        {
            _korisnikService = korisnikService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
 
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<KorisnikPovratniModel>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new ApiResponse(401));

            var korisnik = await _korisnikService.GetKorisnikByEmailAsync(email, string.Empty);

            if (korisnik == null)
                return Unauthorized(new ApiResponse(401));

            return new KorisnikPovratniModel
            {
                Email = korisnik.Email,
                Token = _tokenService.CreateToken(korisnik),
                Alias = korisnik.Alias
            };
        }

        [HttpGet("emailProvjera")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _korisnikService.CheckEmailExistsAsync(email);
        }

        [Authorize]
        [HttpGet("adresa")]
        public async Task<ActionResult<AdresaPovratniModel>> GetUserAddress()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new ApiResponse(401));

            var korisnik = await _korisnikService.GetKorisnikSaAdresomAsync(email);

            if (korisnik == null || korisnik.Adresa == null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Adresa, AdresaPovratniModel>(korisnik.Adresa);
        }

        [Authorize]
        [HttpPut("adresa")]
        public async Task<ActionResult<AdresaPovratniModel>> UpdateUserAddress(AdresaPovratniModel adresa)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized(new ApiResponse(401));

            var korisnik = await _korisnikService.GetKorisnikSaAdresomAsync(email);

            if (korisnik == null)
                return NotFound(new ApiResponse(404));

            korisnik.Adresa = _mapper.Map<AdresaPovratniModel, Adresa>(adresa);

            var result = await _korisnikService.UpdateKorisnikAsync(korisnik);

            if (!result)
                return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Adresa, AdresaPovratniModel>(korisnik.Adresa));
        }

        [HttpPost("prijava")]
        public async Task<ActionResult<KorisnikPovratniModel>> Login(PrijavaPovratniModel prijava)
        {
            var korisnik = await _korisnikService.GetKorisnikByEmailAsync(prijava.Email, prijava.Lozinka);

            // if (korisnik == null || !await _korisnikService.ValidirajLozinkuAsync(korisnik, prijava.Lozinka))
            //     return Unauthorized(new ApiResponse(401));

            if (korisnik == null)
                return Unauthorized(new ApiResponse(401));

            return new KorisnikPovratniModel
            {
                Email = korisnik.Email,
                Token = _tokenService.CreateToken(korisnik),
                Alias = korisnik.Alias
            };
        }

        [HttpPost("registracija")]
        public async Task<ActionResult<KorisnikPovratniModel>> Register(RegistracijaPovratniModel registracija)
        {
            if (await _korisnikService.CheckEmailExistsAsync(registracija.Email))
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "E-mail adresa je u upotrebi" } });
            }

            var korisnik = new Korisnik
            {
                Id = Guid.NewGuid().ToString(),
                Alias = registracija.Alias,
                Email = registracija.Email,
                KorisnickoIme = registracija.Email,
                Lozinka = registracija.Lozinka
                // Lozinka = await _korisnikService.HashirajLozinkuAsync(registracija.Lozinka)
            };

            var result = await _korisnikService.CreateKorisnikAsync(korisnik);

            if (!result)
                return BadRequest(new ApiResponse(400));

            return new KorisnikPovratniModel
            {
                Alias = korisnik.Alias,
                Token = _tokenService.CreateToken(korisnik),
                Email = korisnik.Email
            };
        }
    }
}
