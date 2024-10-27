using System.Collections.Generic;
using System.Threading.Tasks;
using AppDomainModel.Model;

public interface IProizvodRepository
{
    Task<Proizvod> UcitajZaProizvodIdAsync(int id);
    Task<IEnumerable<Proizvod>> UcitajSveProizvodeAsync();
    Task<IEnumerable<RobnaMarka>> UcitajSveRobneMarkeAsync();
    Task<IEnumerable<VrstaProizvoda>> UcitajSveVrsteProizvodaAsync();



    void SnimiProizvod(Proizvod proizvod);
    void AzurirajProizvod(Proizvod proizvod);
    void ObrisiProizvod(int proizvodID);
}