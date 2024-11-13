using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainModel.Models
{
    public class Kosarica
    {
        public Kosarica()
        {
        }

        public Kosarica(string id) 
        {
            Id = id;
            
        }

        public string Id { get; set; }

        public List<KosaricaStavka> Stavke { get; set; } = new List<KosaricaStavka>();
    }
}