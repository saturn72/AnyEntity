using System;
using System.Collections.Generic;
using System.Linq;
using AnyEntity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAnyEntity(this IServiceCollection services, IEnumerable<Type> entitiesToRegister,
         Action<DbContextOptionsBuilder> optionsAction = null)
        {
            if (entitiesToRegister == null || !entitiesToRegister.Any() || entitiesToRegister.All(x => x == null))
                throw new InvalidOperationException($"{nameof(entitiesToRegister)} cannot be null or empty.");

            var etf = new EntityTypeFactory();
            var entities = entitiesToRegister.Where(e => e != null).ToArray();
            etf.RegisterEntities(entities);
            services.AddSingleton(etf);
            AnyEntityDbContext.RegisterAllEntities(entities);

            services.AddScoped<WorkContext>();
            services.AddTransient<DbSetFactory>();
            services.AddTransient<DbContext, AnyEntityDbContext>();
            services.AddTransient<IQueryable<object>>(sp => sp.GetService<DbSetFactory>().GetDbSet());

            services.AddDbContext<AnyEntityDbContext>(optionsAction);
            return services;
        }
    }
}