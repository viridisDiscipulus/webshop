using System.Collections.Generic;
using AppDomainModel.Model;

namespace AppDomainModel.Models
{
    public class KosaricaKupac
    {
        #region Variables
        private string id;
        #endregion

        #region CTOR
        public KosaricaKupac()
        {
        }

        public KosaricaKupac(string id) : base()
        {
            Id = id;
        }
        #endregion

        #region Properties
        public string Id { get => id; set => id = value; }
        public List<KosaricaArtikl> Artikli { get; set; } = new List<KosaricaArtikl>();
        #endregion    
    }
}