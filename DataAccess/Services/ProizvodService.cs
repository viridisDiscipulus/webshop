using System.Collections.Generic;
using AppDomainModel.Model;
using System.Threading.Tasks;

public class ProizvodService : IProizvodService
{
    private readonly IProizvodRepository _productRepository;

    public ProizvodService(IProizvodRepository productRepository)
    {
        _productRepository = productRepository;
    }   

    public void AzurirajProizvod(Proizvod proizvod)
    {
        _productRepository.AzurirajProizvod(proizvod);
    }

    public void SnimiProizvod(Proizvod proizvod)
    {
        _productRepository.SnimiProizvod(proizvod);
    }

    public void ObrisiProizvod(int proizvodId)
    {
        _productRepository.ObrisiProizvod(proizvodId);
    }
    
    public async Task<Proizvod> UcitajZaProizvodIdAsync(int Id)
    {
       return await _productRepository.UcitajZaProizvodIdAsync(Id);
    }
    
    public async Task<IEnumerable<Proizvod>> UcitajSveProizvodeAsync()
    {
        return await _productRepository.UcitajSveProizvodeAsync();
    }

    public async Task<IEnumerable<RobnaMarka>> UcitajSveRobneMarkeAsync()
    {
        return await _productRepository.UcitajSveRobneMarkeAsync();
    }

    public async Task<IEnumerable<VrstaProizvoda>> UcitajSveVrsteProizvodaAsync()
    {
        return await _productRepository.UcitajSveVrsteProizvodaAsync();
    }


}