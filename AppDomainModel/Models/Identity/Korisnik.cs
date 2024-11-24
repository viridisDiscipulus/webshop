using System;

namespace AppDomainModel.Models.Identity
{
    public class Korisnik
    {
        public string Id { get; set; }
        public string Alias { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Email { get; set; }
        public int AdresaId { get; set; } 
        public Adresa Adresa { get; set; }
    }
}