using System.Collections.Generic;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;

namespace ETCQRS.Query.Abstractions.Builder
{
	// ReSharper disable once UnusedTypeParameter
	public abstract class QueryBuilder<T> : IQueryBuilder where T : class, IQuery
	{
		protected readonly IQueryExpressionBuilder ExpressionBuilder;
		protected IQueryDescriptorFactory DescriptorFactory { get; }

		protected IQuery Query { get; private set; }

		protected IQueryDescriptor Descriptor;
		
		protected readonly List<(string, LambdaExpression)> MethodCalls = new List<(string, LambdaExpression)>();

		protected QueryBuilder(IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder)
		{
			ExpressionBuilder = expressionBuilder;
			DescriptorFactory = descriptorFactory;
		}

		public virtual void Init(IQuery query)
		{
			Query = query;
		}

		public virtual void BuildParameter()
		{
			Descriptor = DescriptorFactory.Create(Query.ParameterType);
			ExpressionBuilder.Descriptor = Descriptor;
		}

		public virtual void BuildProperty()
		{
			Descriptor.Property = Expression.Property(Descriptor.Parameter, Query.PropertyName);
		}

		public abstract void BuildExpression();

		public abstract void BuildMethodCalls();

		public virtual (string, LambdaExpression)[] GetResults()
		{
			return MethodCalls.ToArray();
		}
	}
}
