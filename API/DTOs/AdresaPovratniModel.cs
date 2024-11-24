using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class AdresaPovratniModel
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string Ulica { get; set; }

        [Required]
        public string Grad { get; set; }

        [Required]
        public string Drzava { get; set; }

        [Required]
        public string PostanskiBroj { get; set; }
    }
}