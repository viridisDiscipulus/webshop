using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Model;

namespace AppDomainModel.Models.NarudzbeSkupno
{
    public class Adresa : BaseModel
    {
        public Adresa()
        {
        }

        public Adresa(string ime, string prezime, string ulica, string grad, string drzava) 
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.Ulica = ulica;
            this.Grad = grad;
            this.Drzava = drzava;
        }
        
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string PostanskiBroj { get; set; }
    }
}