using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    public static class MvcApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAnyEntity(this IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapGet("{entityName}/filter/{filter}", context =>
          {
              var entityName = context.GetRouteValue("entityName");
              var filter = context.GetRouteValue("filter");
              return context.Response.WriteAsync($"Hi. this is your entity code: GET on -  {entityName}! with filter {filter}");
          });
            routeBuilder.MapGet("{entityName}/{id}", context =>
          {
              var entityName = context.GetRouteValue("entityName");
              var id = context.GetRouteValue("id");
              return context.Response.WriteAsync($"Hi. this is your entity code: GET on - {entityName} with id: {id}!");
          });
            routeBuilder.MapPost("{entityName}", context =>
            {
                var entityName = context.GetRouteValue("entityName");
                return context.Response.WriteAsync($"Hi. this is your entity code: POST on - {entityName}!");
            });

            routeBuilder.MapPut("{entityName}/{id}", context =>
            {
                var entityName = context.GetRouteValue("entityName");
                var id = context.GetRouteValue("id");
                return context.Response.WriteAsync($"Hi. this is your entity code: PUT on - {entityName} with id: {id}!");
            });
            routeBuilder.MapVerb(HttpMethods.Patch, "{entityName}/{id}", context =>
             {
                 var entityName = context.GetRouteValue("entityName");
                 var id = context.GetRouteValue("id");
                 return context.Response.WriteAsync($"Hi. this is your entity code: PATCH on - {entityName} with id: {id}!");
             });
            routeBuilder.MapDelete("{entityName}/{id}", context =>
            {
                var entityName = context.GetRouteValue("entityName");
                var id = context.GetRouteValue("id");
                return context.Response.WriteAsync($"Hi. this is your entity code: DELETE on - {entityName} with id: {id}!");
            });
            var routes = routeBuilder.Build();
            return app.UseRouter(routes);
        }
    }
}