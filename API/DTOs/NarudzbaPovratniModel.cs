using AppDomainModel.Models;

namespace API.DTOs
{
    public class NarudzbaPovratniModel
    {
        public string KosaricaId { get; set; }
        public int NacinIsporukeId { get; set; }
        public AdresaPovratniModel AdresaDostave { get; set; }
        public Placanje Placanje { get; set; }
    }
}