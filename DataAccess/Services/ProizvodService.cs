using System.Collections.Generic;
using AppDomainModel.Model;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;

namespace Services {
    
public class ProizvodService : IProizvodService
{
    private readonly IProizvodRepository _proizvodRepository;

    public ProizvodService(IProizvodRepository proizvodRepository)
    {
        _proizvodRepository = proizvodRepository;
    }   

    public void AzurirajProizvod(Proizvod proizvod)
    {
        _proizvodRepository.AzurirajProizvod(proizvod);
    }

    public void SnimiProizvod(Proizvod proizvod)
    {
        _proizvodRepository.SnimiProizvod(proizvod);
    }

    public void ObrisiProizvod(int proizvodId)
    {
        _proizvodRepository.ObrisiProizvod(proizvodId);
    }
    
    public async Task<Proizvod> UcitajZaProizvodIdAsync(int Id)
    {
       return await _proizvodRepository.UcitajZaProizvodIdAsync(Id);
    }
    
    public async Task<IEnumerable<Proizvod>> UcitajSveProizvodeAsync()
    {
        return await _proizvodRepository.UcitajSveProizvodeAsync();
    }

    public async Task<IEnumerable<RobnaMarka>> UcitajSveRobneMarkeAsync()
    {
        return await _proizvodRepository.UcitajSveRobneMarkeAsync();
    }

    public async Task<IEnumerable<VrstaProizvoda>> UcitajSveVrsteProizvodaAsync()
    {
        return await _proizvodRepository.UcitajSveVrsteProizvodaAsync();
    }


}
}
