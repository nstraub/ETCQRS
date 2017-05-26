using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Builder
{
    public class QueryComposite : IQueryComposite
    {
        private static readonly CallFactory CallFactory;

        static QueryComposite ()
        {
            CallFactory = new CallFactory();
        }

        public QueryComposite ()
        {
            CallChain = new Queue<MethodCallExpression>();
        }

        internal Queue<MethodCallExpression> CallChain { get; set; }
        
        
        public IQueryComposite AddWhereExpression<T> (Expression<Func<T, bool>> expressionResult)
            where T : class
        {
            CallChain.Enqueue(CallFactory.CreateWhere(expressionResult));
            return this;
        }

        public IQueryComposite AddProjectionExpression<TIn, TOut> (Expression<Func<TIn, TOut>> mapper = null)
            where TIn : class
            where TOut : class
        {
            if (mapper is null)
            {
                CallChain.Enqueue(CallFactory.CreateOfType<TIn, TOut>());
            }
            else if (typeof(IEnumerable).IsAssignableFrom(typeof(TOut)))
            {
                CallChain.Enqueue(CallFactory.CreateSelectMany(mapper));
            }
            else
            {
                CallChain.Enqueue(CallFactory.CreateSelect(mapper));
            }

            return this;
        }


        public IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source)
            where TIn : class
            where TOut : class
        {
            return (IQueryable<TOut>) Run(source, CallChain);
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
