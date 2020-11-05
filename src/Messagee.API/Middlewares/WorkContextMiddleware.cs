using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Messagee.API.Security;
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
            _logger.LogInformation(LoggingEvents.WorkContext, $"Start {nameof(WorkContextMiddleware)}.{nameof(InvokeAsync)} invokation");
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var clientId = httpContext.User.FindFirst("client_id")?.Value;

            if (!userId.HasValue() && !clientId.HasValue())
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                _logger.LogDebug($"Missing userId - user is unauthorized!");
                return;
            }
            workContext.CurrentUserId = userId;
            workContext.CurrentClientId = clientId;
            workContext.CurrentRoles = httpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value);
            workContext.Namespaces = httpContext.User.FindAll(MessageeClaimTypes.Namespace)?.Select(c => c.Value);
            _logger.LogDebug(LoggingEvents.WorkContext, "Finish parsing current WorkContext");
            await _next(httpContext);
        }
    }
}