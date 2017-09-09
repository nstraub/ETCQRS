using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Factories;

namespace ETCQRS.Query.Abstractions.Builder
{
	public abstract class QuerylessBuilder<T> : QueryBuilder<T> where T : class, IQuery
	{
		public override void Init(IQuery query)
		{
		}

		public override void BuildParameter()
		{
		}

		public override void BuildProperty()
		{
		}

		public override void BuildExpression()
		{
		}

		protected QuerylessBuilder(IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder, CallFactory callFactory) : base(descriptorFactory, expressionBuilder, callFactory)
		{
		}
	}
}
