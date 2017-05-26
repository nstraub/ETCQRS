using System;
using System.Linq;
using System.Linq.Expressions;

using ETCQRS.Query.Resources;
using ETCQRS.Query.Util;


namespace ETCQRS.Query.Builder
{
    internal class CallFactory
    {
        public MethodCallExpression CreateWhere<T> (Expression<Func<T, bool>> iterator) where T : class
        {
            return Expression.Call(MethodInfos.Where.MakeGenericMethod(typeof(T)), Expression.Parameter(typeof(IQueryable<T>)), iterator);
        }


        public MethodCallExpression CreateSelect<TIn, TOut> (Expression<Func<TIn, TOut>> iterator) where TIn : class where TOut : class
        {
            return Expression.Call(MethodInfos.Select.MakeGenericMethod(typeof(TIn), typeof(TOut)), Expression.Parameter(typeof(IQueryable<TIn>)), iterator);
        }

        public MethodCallExpression CreateSelectMany<TIn, TOut> (Expression<Func<TIn, TOut>> iterator) where TIn : class where TOut : class
        {
            return Expression.Call(MethodInfos.SelectMany.MakeGenericMethod(typeof(TIn), typeof(TOut).GenericTypeArguments[0]), Expression.Parameter(typeof(IQueryable<TIn>)), iterator);
        }

        public MethodCallExpression CreateOfType<TIn, TOut> () where TIn : class where TOut : class
        {
            Ensure.ValidOperation(typeof(TIn).IsAssignableFrom(typeof(TOut)), ErrorMessages.WrongOfTypeCast);
            return Expression.Call(MethodInfos.OfType.MakeGenericMethod(typeof(TOut)), Expression.Parameter(typeof(IQueryable<>).MakeGenericType(typeof(TIn))));
        }
    }
}
