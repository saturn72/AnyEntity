using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AnyEntity
{
    public sealed class WorkContextMiddleware
    {
        private readonly RequestDelegate _next;

        public WorkContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, EntityTypeFactory entityTypeFactory, WorkContext workContext)
        {
            var path = context.Request.Path;
            var urlParts = path.HasValue ? path.Value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries) : null;
            if (urlParts == null || urlParts.Length == 0)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            workContext.EntityType = entityTypeFactory[urlParts[0]];
            await _next(context);
        }
    }
}
