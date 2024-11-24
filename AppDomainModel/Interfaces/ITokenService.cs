using AppDomainModel.Models.Identity;

namespace AppDomainModel.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Korisnik korisnik);
    }
}