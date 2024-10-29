using API.Miscellaneous;
using AppDomainModel.Interfaces;
using AutoMapper;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _conf;
        public Startup(IConfiguration configuration)
        {
            _conf = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGernericService<>), typeof(GenericService<>));                      
           services.AddScoped(typeof(IGernericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProizvodRepository, ProizvodRepository>();
            services.AddScoped<IProizvodService, ProizvodService>();
            services.AddAutoMapper(typeof(ProfiliZaMappiranje));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
