using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainModel.Models;
using AppDomainModel.Models.NarudzbeSkupno;

namespace AppDomainModel.Interfaces
{
    public interface INaplataService
    {
       Task<bool> ProvediTransakciju(Placanje transakcija);
    }
}