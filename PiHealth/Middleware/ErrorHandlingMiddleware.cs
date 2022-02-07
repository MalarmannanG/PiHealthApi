
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PiHealth.Web.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {

            this._next = next;
            this._logger = logger;           
        }

        public async Task Invoke(HttpContext context)
        {
            var cont = context.Request;
            var now = DateTime.Now;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Application Error: {ex.Message}. DATE TIME: {now}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (ex is DTPNotFoundException) code = HttpStatusCode.NotFound;
            //else if (ex is DTPUnAuthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is DTPBadRequestException) code = HttpStatusCode.BadRequest;

            var result =  ex.Message;

            if (code == HttpStatusCode.InternalServerError)
            {
                result = "Internal Error";
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
