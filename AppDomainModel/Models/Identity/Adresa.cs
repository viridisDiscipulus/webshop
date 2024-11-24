namespace AppDomainModel.Models.Identity
{
    public class Adresa
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string PostanskiBroj { get; set; }
        public string KorisnikId { get; set; }
        public Korisnik korisnik { get; set; }
    }
}
