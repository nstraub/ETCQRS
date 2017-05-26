using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryExpressionBuilder : IObservable
    {
        IQueryExpressionBuilder And { get; }
        IQueryExpressionBuilder Or { get; }
        IQueryDescriptor Descriptor { set; }
        IQueryExpressionBuilder AddExpression (Func<Expression, Expression, BinaryExpression> operatorFunc, object value);
        IQueryExpressionBuilder Mutate ();
    }
}
