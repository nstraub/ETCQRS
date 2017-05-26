using System.Linq;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Resources;
using ETCQRS.Query.Util;


namespace ETCQRS.Query.Builder
{
    public class QueryBuildFacade : IQueryBuildFacade
    {
        private IQueryComposite Composite { get; }

        public QueryBuildFacade (IQueryComposite composite)
        {
            Composite = composite;
        }
        
        public void AddQuery (IQuery query, IQueryBuilder queryBuilder)
        {
            queryBuilder.Init(query);
            queryBuilder.BuildParameter();
            queryBuilder.BuildProperty();
            queryBuilder.BuildExpression();
            queryBuilder.BuildMethodCall();
            foreach (var result in queryBuilder.GetResults())
            {
                Composite.CallChain.Enqueue(result);
            }
        }

        public IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source) where TIn : class where TOut : class
        {
            return Composite.Run<TIn, TOut>(source);
        }
    }
}
