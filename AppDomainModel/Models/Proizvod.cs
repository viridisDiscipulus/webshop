namespace AppDomainModel.Model
{
    public class Proizvod : BaseModel 
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public string SlikaUrl { get; set; }
        public VrstaProizvoda VrstaProizvoda { get; set; }
        public int VrstaProizvodaID { get; set; }
        public RobnaMarka RobnaMarka { get; set; }
        public int RobnaMarkaID { get; set; }
    }
}