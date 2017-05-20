using System.Linq;


namespace ETCQRS.Query.Abstractions.Base
{
    public interface IQueryDispatcher
    {
        IQueryable<TOut> Dispatch<TIn, TOut> (IQueryable<TIn> source, params IQuery[] queries)
            where TIn : class
            where TOut : class, IQueryResult;
    }
}
