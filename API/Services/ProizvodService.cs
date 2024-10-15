using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using AppDomainModel.Model;
using System.Threading.Tasks;
using DataAccess.Repositories;  

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

    public void ObrisiProizvod(int proizvodID)
    {
        _productRepository.ObrisiProizvod(proizvodID);
    }

    public async Task<IEnumerable<Proizvod>> UcitajSveProizvode()
    {
        return await _productRepository.UcitajSveProizvode();
    }

    public async Task<Proizvod> UcitajZaProizvodID(int id)
    {
       return await _productRepository.UcitajZaProizvodID(id);
    }
}