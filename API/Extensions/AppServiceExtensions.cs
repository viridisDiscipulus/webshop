using System.Linq;
using API.ErrorTypes;
using AppDomainModel.Interfaces;
using DataAccess.Repositories;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProizvodRepository, ProizvodRepository>();
            services.AddScoped<IProizvodService, ProizvodService>();
            services.AddScoped<IKosaricaRepository, KosaricaRepository>();
            services.AddTransient<IKorisnikRepository, KorisnikRepository>();
            services.AddTransient<IKorisnikService, KorisnikService>();
            services.AddTransient<ITokenService, TokenService>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }

    }
}