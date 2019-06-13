using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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
            if (!path.HasValue)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }
            path = path.Value.TrimStart('/');

            var u = path.ToUriComponent();
            // var entityName = route.Values["entityName"].ToString();
            // workContext.EntityType = entityTypeFactory[entityName];
            await _next(context);
        }
    }
}
