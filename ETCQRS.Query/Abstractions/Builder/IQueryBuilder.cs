using System;
using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryBuilder : IObservable
    {
        Func<Expression, Expression, BinaryExpression> QueryLinker { set; }
        void AddExpression (IQueryDescriptor descriptor, Func<Expression, Expression, BinaryExpression> operatorFunc, object value);
    }
}
