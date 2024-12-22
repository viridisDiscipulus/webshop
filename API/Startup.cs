using System.Data;
using System.IO;
using Api.Extensions;
using API.Extensions;
using API.Middleware;
using API.Miscellaneous;
using AppDomainModel.Interfaces;
using AutoMapper;
using DataAccess.Repositories;
using DataAccess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));                      
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(ProfiliZaMappiranje));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    // builder => builder.WithOrigins("http://localhost:4200") // URL Angular aplikacije
                    //                 .AllowAnyMethod()
                    //                 .AllowAnyHeader());
                        builder => builder.SetIsOriginAllowed(origin => true) 
                        .AllowAnyMethod()                   
                        .AllowAnyHeader()                  
                        .AllowCredentials());
            });

            services.AddControllers();
            
            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddSingleton<IDbConnection>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            });

            services.AddCustomIdentityServices(_config);
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
            app.UseStaticFiles(new StaticFileOptions{
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Sadrzaj")),
                RequestPath = "/Sadrzaj"
            });

             // Enable CORS
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseSwagger();
            // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop MalaSapa API v1"));
            app.UseSwaggerDocumentation();
    

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
