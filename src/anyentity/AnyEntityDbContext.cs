using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AnyEntity
{
    public sealed class AnyEntityDbContext : DbContext
    {
        private static readonly IDictionary<Type, IQueryable<object>> AllDbSets = new Dictionary<Type, IQueryable<object>>();
        private static Action<ModelBuilder> _onModelCreatingAction;

        public static void ConfigureOnModelCreating(Action<ModelBuilder> onModelCreatingAction)
        {
            _onModelCreatingAction = onModelCreatingAction;
        }
        public AnyEntityDbContext(DbContextOptions<AnyEntityDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public IQueryable<object> Set(Type type)
        {
            if (AllDbSets.TryGetValue(type, out IQueryable<object> dbSet))
                return dbSet as DbSet<object>;
            dbSet = ExtractDbSet(type);
            if (dbSet != null)
                AllDbSets[type] = dbSet;
            return dbSet;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            _onModelCreatingAction(builder);
        }
        private IQueryable<object> ExtractDbSet(Type type)
        {
            var pis = typeof(DbContext).GetProperties().Select(x => x.Name).ToArray();
            var mis = typeof(DbContext).GetMethods().Select(x => x.Name).ToArray();
            var mi = typeof(DbContext)
                .GetMethod(nameof(DbContext.Set))
                .MakeGenericMethod(type);
            var res = mi.Invoke(this, null);
            return res as IQueryable<object>;
        }
    }
}