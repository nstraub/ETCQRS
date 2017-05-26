using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Builder;


namespace ETCQRS.Query.Util
{
    [Localizable(false)]
    public class SampleDispatcher : IQueryDispatcher
    {
        private readonly IQueryBuildFacade _facade;
        private readonly Dictionary<string, Type> _queryBuilders = new Dictionary<string, Type>();

        public SampleDispatcher (IQueryBuildFacade facade, params string[] assemblyNames)
        {
            _facade = facade;
            IList<Type> types = AppDomain.CurrentDomain.GetAssemblies().Where(a => assemblyNames.Any(an => a.FullName.Contains(an))).SelectMany(a => a.GetTypes()).ToList();
            var queries = types.Where(t => typeof(IQuery).IsAssignableFrom(t)).ToList();
            var queryBuilders = types.Where(t => typeof(IQueryBuilder).IsAssignableFrom(t));

            foreach (var queryBuilder in queryBuilders)
            {
                if (queries.Any(q => queryBuilder.Name == q.Name + "Builder"))
                {
                    _queryBuilders.Add(queryBuilder.Name, queryBuilder);
                }
            }
        }
        public IQueryable<TOut> Dispatch<TIn, TOut> (IQueryable<TIn> source, params IQuery[] queries) where TIn : class where TOut : class, IQueryResult
        {
            foreach (var query in queries)
            {
                var builderType = _queryBuilders[query.GetType().Name + "Builder"];
                var descriptorFactory = new QueryDescriptorFactory(new MutatorFlyweightFactory());
                var queryExpressionBuilder = new QueryExpressionBuilder();

                var builder = Activator.CreateInstance(builderType, descriptorFactory, queryExpressionBuilder);

                _facade.AddQuery(query, (IQueryBuilder)builder);
            }

            return _facade.Run<TIn, TOut>(source);
        }
    }
}
