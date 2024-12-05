using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Models.NarudzbeSkupno
{
    public class Narudzba : BaseModel
    {
        public Narudzba()
         {
        }

        public Narudzba(List<NaruceniArtikl> naruceniArtikli, string kupacEmail, Adresa adresaDostave, NacinIsporuke nacinIsporuke, decimal ukupnaCijena) 
        {
            this.NaruceniArtikli = naruceniArtikli;
            this.NacinIsporuke = nacinIsporuke;
            this.KupacEmail = kupacEmail;
            this.AdresaDostave = adresaDostave;
            this.UkupnaCijena = ukupnaCijena;
        }

        public Narudzba(List<NaruceniArtikl> naruceniArtikli, string kupacEmail, Adresa adresaDostave, NacinIsporuke nacinIsporuke, decimal ukupnaCijena, string naruceniArtikliId) 
        {
            this.NaruceniArtikli = naruceniArtikli;
            this.NacinIsporuke = nacinIsporuke;
            this.KupacEmail = kupacEmail;
            this.AdresaDostave = adresaDostave;
            this.UkupnaCijena = ukupnaCijena;
            this.NaruceniArtikliId = naruceniArtikliId;
        }
        
        public string KupacEmail { get; set; }
        public DateTimeOffset DatumNarudzbe { get; set; } = DateTimeOffset.Now;
        public Adresa AdresaDostave { get; set; }
        public NacinIsporuke NacinIsporuke { get; set; }
        public List<NaruceniArtikl> NaruceniArtikli { get; set; }
        public decimal UkupnaCijena { get; set; }
        public StatusNarudzbe Status { get; set; }
        public string NaruceniArtikliId { get; set; }
    
        public decimal PunaCijena() {
            return UkupnaCijena + NacinIsporuke.Cijena;
        }
    }
}