using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppDomainModel.Interfaces;
using AppDomainModel.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(Korisnik korisnik)
        {
            var claims = new List<Claim>
            {
                new Claim("alias", korisnik.Alias),
                new Claim("email", korisnik.Email),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                Issuer = _config["Token:Issuer"],
                SigningCredentials = creds,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        // public ClaimsPrincipal ValidateToken(string token)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var validationParameters = new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = _key,
        //         ValidIssuer = _config["Token:Issuer"],
        //         ValidateIssuer = true,
        //         ValidateAudience = false
        //     };

        //     try
        //     {
        //         var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

        //         // Ensure the token is a valid JWT token
        //         if (validatedToken is JwtSecurityToken jwtToken && 
        //             jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //         {
        //             return principal;
        //         }

        //         throw new SecurityTokenException("Invalid token");
        //     }
        //     catch
        //     {
        //         return null;
        //     }
        // }
    }
}
