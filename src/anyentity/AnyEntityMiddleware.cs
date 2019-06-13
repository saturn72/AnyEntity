using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AnyEntity
{
    public class AnyEntityMiddleware
    {
        private readonly RequestDelegate _next;

        public AnyEntityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}