using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryBuilder
    {
        Func<Expression, Expression, BinaryExpression> QueryLinker { set; }
        void AddExpression (IQueryDescriptor descriptor, Func<Expression, Expression, BinaryExpression> operatorFunc, object value);

        IQueryBuilder SetMutator (IQueryDescriptor descriptor, IExpressionMutator mutator);

        IQueryBuilder Mutate (IQueryDescriptor descriptor);
    }
}
