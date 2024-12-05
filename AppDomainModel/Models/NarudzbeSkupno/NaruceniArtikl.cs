using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Models.NarudzbeSkupno
{
    public class NaruceniArtikl : BaseModel
    {
        public NaruceniArtikl()
        {
        }


        public NaruceniArtikl(NarucenProizvodArtikl naruceniArtikl, decimal cijena, int kolicina) 
        {
            ArtiklNaruceni = naruceniArtikl;
            Cijena = cijena;
            Kolicina = kolicina;
        }

        public NarucenProizvodArtikl ArtiklNaruceni { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
    }
}