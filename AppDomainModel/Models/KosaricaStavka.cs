using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainModel.Models
{
    public class KosaricaStavka
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public string SlikaUrl { get; set; }
        public string RobnaMarka { get; set; }
        public string VrstaProizvoda { get; set; }
    }
}