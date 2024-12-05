using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Models.NarudzbeSkupno
{
    public class NacinIsporuke: BaseModel
    {
        public string KratkiNaziv { get; set; }
        public string VrijemeDostave { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
    }
}