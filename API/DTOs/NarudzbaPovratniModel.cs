using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class NarudzbaPovratniModel
    {
        public string KosaricaId { get; set; }
        public int NacinIsporukeId { get; set; }
        public AdresaPovratniModel AdresaDostave { get; set; }
    }
}