using System;
using System.Collections.Generic;
using System.Linq;
using AnyEntity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAnyEntity(this IServiceCollection services, IEnumerable<Type> entitiesToRegister)
        {
            if (entitiesToRegister == null || !entitiesToRegister.Any() || entitiesToRegister.All(x => x == null))
                throw new InvalidOperationException($"{nameof(entitiesToRegister)} cannot be null or empty.");

            var etf = new EntityTypeFactory();
            etf.RegisterEntities(entitiesToRegister.Where(e => e != null).ToArray());
            services.AddSingleton(etf);
            services.AddScoped<WorkContext>();
            services.AddTransient<DbContext>();

            return services;
        }
    }
}