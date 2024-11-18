using AppDomainModel.Model;

namespace AppDomainModel.Models
{
    public class KosaricaStavka : BaseModel
    {
        public string ProizvodNaziv { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public string SlikaUrl { get; set; }
        public string RobnaMarka { get; set; }
        public string VrstaProizvoda { get; set; }

    }
}