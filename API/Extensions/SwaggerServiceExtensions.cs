using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "WebShop MalaSapa API", 
                    Version = "v1",
                    Description = "Interna API dokumentacija. Ne eksponirajte javno." 
                });

                
                // var securitySchema = new OpenApiSecurityScheme
                // {
                //     Description = "JWT Auth Bearer Scheme (Koristite bilo koji token za testiranje)", 
                //     Name = "Autorizacija",
                //     In = ParameterLocation.Header,
                //     Type = SecuritySchemeType.Http,
                //     Scheme = "bearer"
                // };

                // c.AddSecurityDefinition("Bearer", securitySchema); 
            });

        return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            // Swagger je dostupan bez provjere autenticnosti u proizvodnji
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop MalaSapa API v1"));

            return app;
        }
    }
}