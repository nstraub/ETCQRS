using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;
using System.Linq.Expressions;

namespace ETCQRS.Query.Abstractions.Builder
{
	public abstract class WhereQueryBuilder<T> : QueryBuilder<T> where T : class, IQuery
	{
		public override void BuildMethodCalls()
		{
			MethodCalls.Add(("Where", Expression.Lambda(Descriptor.Query, Descriptor.Parameter)));
		}

		protected WhereQueryBuilder(IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder) : base(descriptorFactory, expressionBuilder)
		{
		}
	}
}
