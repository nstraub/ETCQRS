using System.Linq;

namespace ETCQRS.Query.Abstractions.Base
{
	public interface IQueryDispatcher
	{
		IQueryDispatcher Prepare(params string[] queryStrings);

		IQueryDispatcher Prepare(params IQuery[] queries);

		IQueryable<T> Dispatch<T>(params IQuery[] queries)
			where T : class, IQueryResult;

		IQueryable<TOut> Dispatch<TIn, TOut>(params IQuery[] queries)
			where TIn : class
			where TOut : class, IQueryResult;
	}
}
