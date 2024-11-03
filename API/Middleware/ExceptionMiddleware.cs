using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.ErrorTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware 
{
    public class ExceptionMiddleware
    {
        #region Properties
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        #endregion

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var camelCaseOpcija = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, camelCaseOpcija);

                await context.Response.WriteAsync(json);
            }
        }

    }
}