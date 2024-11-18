using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class KosaricaController : BaseApiController
    {
        private readonly IKosaricaRepository _kosaricaRepository;
        private readonly IMapper _mapper;

        public KosaricaController(IKosaricaRepository kosaricaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _kosaricaRepository = kosaricaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<KosaricaKupac>> GetKosaricaKupac(string id)
        {
            var kosarica = await _kosaricaRepository.GetKosaricaKupacAsync(id);

            return Ok(kosarica ?? new KosaricaKupac(id));
        }

        [HttpPost]
        public async Task<ActionResult<KosaricaKupac>> UpdateKosaricaKupac(KosaricaKupac kosarica)
        {
            var updatedKosarica = await _kosaricaRepository.UpdateKosaricaKupacAsync(kosarica);

            return Ok(updatedKosarica);
        }

        [HttpDelete]
        public async Task DeleteKosaricaKupac(string id)
        {
            await _kosaricaRepository.DeleteKosaricaKupacAsync(id);
        }       

    }
}