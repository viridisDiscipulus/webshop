using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Models;

namespace DataAccess.Services
{
    public class NaplataService : INaplataService
    {
        private readonly IGenericService<Placanje> _placanjeService;

        public NaplataService(IGenericService<Placanje> placanjeService)
        {
            _placanjeService = placanjeService;
        }

        public async Task<bool> ProvediTransakciju(Placanje transakcija)
        {
        //   string checkQuery = @"
        //         SELECT Id,
        //             VlasnikKartice,
        //             BrojKartice,
        //             DatumIsteka,
        //             CVV
        //         FROM dbo.Placanje;";

            // var lista = await _placanjeService.UcitajSveAsync(checkQuery,dr => new Placanje   
            //     {
            //         Id = (int)dr["Id"],
            //         BrojKartice = (string)dr["BrojKartice"],
            //         VlasnikKartice = (string)dr["VlasnikKartice"],
            //         DatumIsteka = (string)dr["DatumIsteka"],
            //         CVV = (string)dr["CVV"]
            //     });
          
            // return lista.Any(x => x.BrojKartice == transakcija.BrojKartice && x.VlasnikKartice == transakcija.VlasnikKartice && x.DatumIsteka == transakcija.DatumIsteka && x.CVV == transakcija.CVV);

            string checkQuery = $@"
                SELECT Id,
                    VlasnikKartice,
                    BrojKartice,
                    DatumIsteka,
                    CVV
                FROM dbo.Placanje
                WHERE BrojKartice = '{transakcija.BrojKartice}' 
                AND VlasnikKartice = '{transakcija.VlasnikKartice}' 
                AND DatumIsteka = '{transakcija.DatumIsteka}' 
                AND CVV = '{transakcija.CVV}';";

            var lista = await _placanjeService.UcitajSveAsync(checkQuery, dr => new Placanje   
                {
                    Id = (int)dr["Id"],
                    BrojKartice = (string)dr["BrojKartice"],
                    VlasnikKartice = (string)dr["VlasnikKartice"],
                    DatumIsteka = (string)dr["DatumIsteka"],
                    CVV = (string)dr["CVV"]
                });

            return lista.Any();
       
        }
    }
}