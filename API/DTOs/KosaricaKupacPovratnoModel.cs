using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class KosaricaKupacPovratnoModel
    {
        [Required]
        public string Id { get; set; }
        public List<KosaricaArtiklPovratniModel> Artikli { get; set; }
        public int? VrstaDostaveId { get; set; }
    }
}