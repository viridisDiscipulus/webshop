using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class NaruceniArtikliPovratniModel
    {
        public int ProizvodId { get; set; }
        public string ProizvodNaziv { get; set; }
        public string SlikaUrl { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
    }
}