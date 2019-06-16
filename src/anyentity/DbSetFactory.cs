using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AnyEntity
{
    public sealed class DbSetFactory
    {
        private static readonly IDictionary<Type, IQueryable<object>> AllDbSets = new Dictionary<Type, IQueryable<object>>();
        private readonly WorkContext _workContext;
        private readonly DbContext _dbContext;

        public DbSetFactory(WorkContext workContext, DbContext dbContext)
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }
        public IQueryable<object> GetDbSet()
        {
            if (AllDbSets.TryGetValue(_workContext.EntityType, out IQueryable<object> queryable))
                return queryable;
            var t = _workContext.EntityType;
            queryable = ExtractDbSet(t);
            if (queryable != null)
                AllDbSets[t] = queryable;
            return queryable;
        }
        private IQueryable<object> ExtractDbSet(Type type)
        {
            var mi = _dbContext
                .GetType()
                .GetMethod(nameof(DbContext.Set))
                .MakeGenericMethod(type);
            var res = mi.Invoke(_dbContext, null);
            return res as IQueryable<object>;
        }
    }
}