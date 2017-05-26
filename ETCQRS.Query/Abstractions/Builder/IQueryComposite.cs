using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryComposite
    {
        Queue<MethodCallExpression> CallChain { get; }
        IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source)
            where TIn : class
            where TOut : class;
    }
}
