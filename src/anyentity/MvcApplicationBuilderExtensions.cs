using AnyEntity;
namespace Microsoft.AspNetCore.Builder
{
    public static class MvcApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAnyEntity(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WorkContextMiddleware>();
        }
    }
}