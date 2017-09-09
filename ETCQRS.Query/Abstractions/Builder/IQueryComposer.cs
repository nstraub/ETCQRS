using ETCQRS.Query.Abstractions.Base;
using System.Linq;

namespace ETCQRS.Query.Abstractions.Builder
{
	public interface IQueryComposer
	{
		void addSeed<T>(IQueryable<T> seed) where T : class;

		void AddQuery(IQuery query, IQueryBuilder queryBuilder);

		IQueryable<TOut> Run<TIn, TOut>(IQueryable<TIn> source) where TIn : class where TOut : class;
	}
}
