using AutoMapper.Configuration.Conventions;

namespace API.DTOs
{
    public class ProizvodPovratniModel 
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public string SlikaUrl { get; set; }
        public string VrstaProizvoda { get; set; }
        public string RobnaMarka { get; set; }
    }
}