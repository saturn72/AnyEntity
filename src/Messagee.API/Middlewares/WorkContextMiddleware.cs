using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Messagee.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Messagee.API.Middlewares
{
    public class WorkContextMiddleware
    {
        private readonly ILogger<WorkContextMiddleware> _logger;
        private readonly RequestDelegate _next;
        public WorkContextMiddleware(RequestDelegate next, ILogger<WorkContextMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, WorkContext workContext)
        {
            _logger.LogDebug(LoggingEvents.WorkContext, "Start WorkContextMiddleware invokation");
            var clientId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!clientId.HasValue())
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                _logger.LogDebug($"Missing userId - user is unauthorized!");
                return;
            }
            workContext.CurrentClientId = clientId;
            _logger.LogDebug(LoggingEvents.WorkContext, "Finish parsing current WorkContext");
            await _next(httpContext);
        }
    }
}