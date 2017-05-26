﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Util
{
    public class QueryFlyweightFactory : IFlyweightFactory<IQuery>
    {
        private readonly IDictionary<string, Type> _queries = new Dictionary<string, Type>();
        private readonly IDictionary<string, IQuery> _flyweights = new Dictionary<string, IQuery>();

        public QueryFlyweightFactory(params string[] assemblyNames) : this(AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assemblyNames.Any(an => assembly.FullName.Contains(an))).SelectMany(assembly => assembly.GetTypes()).Where(type => typeof(IQuery).IsAssignableFrom(type)))
        {
        }

        public QueryFlyweightFactory (IEnumerable<Type> queries)
        {
            bool OnlyHasOneParameterlessConstructor (ConstructorInfo[] constructors) => constructors.Length == 1 && constructors [0].GetParameters().Length == 0;

            foreach (var query in queries.Where(q => OnlyHasOneParameterlessConstructor(q.GetConstructors())))
            {
                _queries.Add(query.Name, query);
            }

            
        }

        public IQuery Get (string key)
        {
            try
            {
                return _flyweights.ContainsKey(key) ? _flyweights[key] : BuildCurrent(key);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"The given key, \"{key}\" was not present in the dictionary.", e);
            }
        }

        private IQuery BuildCurrent (string key)
        {
            var currentType = _queries[key];
            var current = (IQuery) Activator.CreateInstance(currentType);
            _flyweights.Add(key, current);
            return current;
        }
    }
}
