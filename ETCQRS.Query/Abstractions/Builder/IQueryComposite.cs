using System;
using System.Linq;
using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryComposite
    {
        IQueryComposite AddWhereExpression<T> (Expression<Func<T, bool>> expressionResult)
            where T : class;

        IQueryComposite AddProjectionExpression<TIn, TOut> (Expression<Func<TIn, TOut>> mapper = null)
            where TIn : class
            where TOut : class;

        IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source)
            where TIn : class
            where TOut : class;
    }
}
