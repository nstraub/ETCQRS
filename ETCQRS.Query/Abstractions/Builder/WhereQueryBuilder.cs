using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Abstractions.Builder
{
    public abstract class WhereQueryBuilder<T, TIn> : QueryBuilder<T> where T : class, IQuery where TIn : class
    {
        public override void BuildMethodCalls ()
        {
            MethodCall.Add(CallFactory.CreateWhere((Expression<Func<TIn, bool>>)Expression.Lambda(Descriptor.Query, Descriptor.Parameter)));
        }

        protected WhereQueryBuilder (IQueryDescriptorFactory descriptorFactory, IQueryExpressionBuilder expressionBuilder) : base(descriptorFactory, expressionBuilder) { }
    }
}
