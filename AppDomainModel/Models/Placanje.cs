using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Models
{
    public class Placanje : BaseModel
    {
        public string VlasnikKartice { get; set; }
        public string BrojKartice { get; set; }
        public string DatumIsteka { get; set; }
        public string CVV { get; set; }
    }
}