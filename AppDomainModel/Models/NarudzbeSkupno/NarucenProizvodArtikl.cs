using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Models.NarudzbeSkupno
{
    public class NarucenProizvodArtikl : BaseModel
    {
        public NarucenProizvodArtikl()
        {
        }

        public NarucenProizvodArtikl(int proizvodArtiklId, string proizvodNaziv, string slikaUrl) 
        {
            ProizvodArtiklId = proizvodArtiklId;
            ProizvodNaziv = proizvodNaziv;
            SlikaUrl = slikaUrl;
        }

        public int ProizvodArtiklId { get; set; }
        public string ProizvodNaziv { get; set; }
        public string SlikaUrl { get; set; }
    }
}