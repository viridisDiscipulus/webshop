using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers
{
    public class NarudzbaPovratPovratniModel
    {
            public int Id { get; set; }
            public string KupacEmail { get; set; }
            // public DateTimeOffset DatumNarudzbe { get; set; }
            public string DatumNarudzbe { get; set; }
            public AppDomainModel.Models.NarudzbeSkupno.Adresa AdresaDostave { get; set; }
            public string NacinIsporuke { get; set; }
            public decimal CijenaDostave { get; set; }
            public IReadOnlyList<NaruceniArtikliPovratniModel> NaruceniArtikli { get; set; }
            public decimal UkupnaCijena { get; set; }
            public string Status { get; set; }
            public decimal SveukupnaCijena { get; set; } 
    }

}