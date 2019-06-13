using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAnyEntity(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddRouting();
        }
    }
}
