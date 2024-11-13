using System.Linq;
using Api.Extensions;
using API.ErrorTypes;
using API.Extensions;
using API.Middleware;
using API.Miscellaneous;
using AppDomainModel.Interfaces;
using AutoMapper;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Services;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGernericService<>), typeof(GenericService<>));                      
            services.AddScoped(typeof(IGernericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(ProfiliZaMappiranje));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200") // Promijeniti ovo u URL Angular aplikacije
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            services.AddControllers();

            services.AddSingleton<ConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_config
                    .GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ExceptionMiddleware>();

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

             // Enable CORS
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            // app.UseSwagger();
            // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop MalaSapa API v1"));
            app.UseSwaggerDocumentation();
    

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
