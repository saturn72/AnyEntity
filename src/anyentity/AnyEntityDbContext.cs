using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnyEntity
{
    public sealed class AnyEntityDbContext : DbContext
    {
        public AnyEntityDbContext(DbContextOptions<AnyEntityDbContext> options)
            : base(options)
        { }
        private static Action<ModelBuilder> _onModelCreatingAction;
        internal static void RegisterAllEntities(IEnumerable<Type> entities)
        {
            _onModelCreatingAction = builder =>
            {
                foreach (var e in entities)
                    builder.Entity(e)
                    .HasIndex(nameof(AnyEntityModelBase<object>.Id));
            };
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            _onModelCreatingAction(builder);
        }
    }
}