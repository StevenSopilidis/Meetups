using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //if any exception was thrown from middleware stack
                _logger.LogError(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()? 
                    new AppException(ex.Message, context.Response.StatusCode, ex.StackTrace.ToString())
                    : new AppException(ex.Message, context.Response.StatusCode, ex.StackTrace.ToString());

                //set the json naming policy to camel case 
                var options = new JsonSerializerOptions{PropertyNamingPolicy= JsonNamingPolicy.CamelCase};

                var jsonResponse = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}