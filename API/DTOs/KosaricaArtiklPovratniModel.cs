using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class KosaricaArtiklPovratniModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Cijena mora biti veća od 0")]
        public decimal Cijena { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Količina mora biti veća od 0")]
        public int Kolicina { get; set; }
        [Required]
        public string SlikaUrl { get; set; }
        [Required]
        public string VrstaProizvoda { get; set; }
        [Required]
        public string RobnaMarka { get; set; }
    }
}