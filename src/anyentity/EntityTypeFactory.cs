using System;
using System.Collections.Generic;

namespace AnyEntity
{
    public sealed class EntityTypeFactory
    {
        private readonly IDictionary<string, Type> _data;

        public EntityTypeFactory()
        {
            _data = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        }

        public Type this[string key]
        {
            get
            {
                _data.TryGetValue(key, out Type value);
                return value;
            }
        }
        public void RegisterEntities(IEnumerable<Type> entities)
        {
            foreach (var e in entities)
                _data.Add(e.Name, e);
        }
    }
}
