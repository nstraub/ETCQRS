using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Factories;


namespace ETCQRS.Query.Abstractions.Builder
{
	public abstract class WhereQueryBuilder<T, TIn> : QueryBuilder<T> where T : class, IQuery where TIn : class
	{
		public override void BuildMethodCalls()
		{
			MethodCall.Add(CallFactory.CreateWhere((Expression<Func<TIn, bool>>)Expression.Lambda(Descriptor.Query, Descriptor.Parameter)));
		}


		protected WhereQueryBuilder(IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder, CallFactory callFactory) : base(descriptorFactory, expressionBuilder, callFactory)
		{
		}
	}
}
