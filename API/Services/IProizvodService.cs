using System.Collections.Generic;
using System.Threading.Tasks;
using AppDomainModel.Model;

public interface IProizvodService 
{
    Task<Proizvod> UcitajZaProizvodID(int id);
    Task<IEnumerable<Proizvod>> UcitajSveProizvode();
    void SnimiProizvod(Proizvod proizvod);
    void AzurirajProizvod(Proizvod proizvod);
    void ObrisiProizvod(int proizvodID);
}