using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using System;
using System.Linq.Expressions;

namespace ETCQRS.Query.Builder
{
	public class QueryDescriptor : IQueryDescriptor
	{
		public QueryDescriptor(Type type, IFlyweightFactory<IExpressionMutator> mutators)
		{
			Parameter = Expression.Parameter(type);
			Mutator = mutators.Get("Throwing");
		}

		private IExpressionMutator Mutator { get; set; }

		public void SetMutator(IExpressionMutator mutator)
		{
			Mutator = mutator;
		}

		public void Mutate()
		{
			Mutator.Execute(this);
		}

		public virtual BinaryExpression Query { get; set; }
		public ParameterExpression Parameter { get; }
		public MemberExpression Property { get; set; }
	}
}
