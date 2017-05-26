using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Builder
{
    public class QueryComposite : IQueryComposite
    {
        private static readonly MethodInfo SelectMethodInfo;
        private static readonly MethodInfo WhereMethodInfo;
        private static readonly MethodInfo SelectManyMethodInfo;
        private static readonly MethodInfo OfTypeMethodInfo;

        static QueryComposite ()
        {
            var methodInfos = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);

            SelectMethodInfo = methodInfos.First(m => m.Name == "Select" && m.GetParameters().Length == 2);
            SelectManyMethodInfo = methodInfos.First(m => m.Name == "SelectMany" && m.GetParameters().Length == 2);
            WhereMethodInfo = methodInfos.First(m => m.Name == "Where" && m.GetParameters().Length == 2);
            OfTypeMethodInfo = methodInfos.First(m => m.Name == "OfType" && m.GetParameters().Length == 1);
        }

        public QueryComposite ()
        {
            CallChain = new Queue<MethodCallExpression>();
        }

        internal Queue<MethodCallExpression> CallChain { get; set; }
        
        
        public IQueryComposite AddWhereExpression<T> (Expression<Func<T, bool>> expressionResult)
            where T : class
        {
            CallChain.Enqueue(Expression.Call(WhereMethodInfo.MakeGenericMethod(typeof(T)), Expression.Parameter(typeof(IQueryable<T>)), expressionResult));
            return this;
        }

        public IQueryComposite AddProjectionExpression<TIn, TOut> (Expression<Func<TIn, TOut>> mapper = null)
            where TIn : class
            where TOut : class
        {
//            Contract.Requires<InvalidOperationException>(mapper != null || typeof(TIn).IsAssignableFrom(typeof(TOut)), "WrongOfTypeCast");

            var tin = typeof(TIn);

            var tout = typeof(TOut);
            if (mapper is null)
            {
                Enqueue(OfTypeMethodInfo.MakeGenericMethod(tout), Expression.Parameter(typeof(IQueryable<>).MakeGenericType(tin)));
            }
            else
            {
                var parameter = Expression.Parameter(typeof(IQueryable<TIn>));
                var selectMany = typeof(IEnumerable).IsAssignableFrom(tout);

                Enqueue((selectMany ? SelectManyMethodInfo : SelectMethodInfo).MakeGenericMethod(tin, selectMany ? tout.GenericTypeArguments [0] : tout), parameter, mapper);
            }
            return this;
        }


        public IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source)
            where TIn : class
            where TOut : class
        {
            return (IQueryable<TOut>) Run(source, CallChain);
        }

        private void Enqueue (MethodInfo genericMethod, ParameterExpression parameter) { CallChain.Enqueue(Expression.Call(genericMethod, parameter)); }

        private void Enqueue<TIn, TOut> (MethodInfo genericMethod, ParameterExpression parameter, Expression<Func<TIn, TOut>> mapper)
            where TIn : class
            where TOut : class
        {
            CallChain.Enqueue(Expression.Call(genericMethod, parameter, mapper));
        }

        private IQueryable Run (IQueryable source, Queue<MethodCallExpression> expressions)
        {
            IQueryable NextInLine () { return source.Provider.CreateQuery(UpdateCall(source, CallChain.Dequeue())); }

            return expressions.Count > 1 ? Run(NextInLine(), CallChain) : NextInLine();
        }

        private MethodCallExpression UpdateCall (IQueryable source, MethodCallExpression methodCallExpression)
        {
            return methodCallExpression.Update(null, new List<Expression>
                                                     {
                                                         Expression.Constant(source)
                                                     }.Concat(StripParameters(methodCallExpression)));
        }

        private IEnumerable<Expression> StripParameters (MethodCallExpression methodCallExpression) { return methodCallExpression.Arguments.Where(arg => arg.NodeType != ExpressionType.Parameter); }
    }
}
