using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryExpressionBuilder : IObservable
    {
        Func<Expression, Expression, BinaryExpression> QueryLinker { set; }
        void AddExpression (IQueryDescriptor descriptor, Func<Expression, Expression, BinaryExpression> operatorFunc, object value);
    }
}
