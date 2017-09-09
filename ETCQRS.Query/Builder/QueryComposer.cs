using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ETCQRS.Query.Builder
{
	public class QueryComposer : IQueryComposer
	{
		private readonly CallFactory _callFactory;
		private IQueryable<object> _seed;
		private Queue<(string, LambdaExpression)> CallChain { get; }

		public QueryComposer(CallFactory callFactory)
		{
			_callFactory = callFactory;
			CallChain = new Queue<(string, LambdaExpression)>();
		}

		public void addSeed<T>(IQueryable<T> seed) where T : class
		{
			_seed = seed;
		}

		public void AddQuery(IQuery query, IQueryBuilder queryBuilder)
		{
			queryBuilder.Init(query);
			queryBuilder.BuildParameter();
			queryBuilder.BuildProperty();
			queryBuilder.BuildExpression();
			queryBuilder.BuildMethodCalls();

			foreach (var result in queryBuilder.GetResults())
			{
				CallChain.Enqueue(result);
			}
		}

		public IQueryable<TOut> Run<TIn, TOut>(IQueryable<TIn> source) where TIn : class where TOut : class
		{
			return (IQueryable<TOut>)(CallChain.Count > 0 ? Run(source, CallChain) : source);
		}

		private IQueryable Run(IQueryable source, Queue<(string, LambdaExpression)> expressions)
		{
			IQueryable NextInLine()
			{
				var currentExpression = expressions.Dequeue();
				return _callFactory.Create[currentExpression.Item1].Invoke(source, currentExpression.Item2);
			}

			return expressions.Count > 1 ? Run(NextInLine(), expressions) : NextInLine();
		}
	}
}
