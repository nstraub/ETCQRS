using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Builder
{
    public class QueryComposer : IQueryComposer
    {
        private Queue<MethodCallExpression> CallChain { get; }
        public QueryComposer ()
        {
            CallChain = new Queue<MethodCallExpression>();
        }


        public void AddQuery (IQuery query, IQueryBuilder queryBuilder)
        {
            queryBuilder.InitProperties(query);
            queryBuilder.Init(query);
            queryBuilder.BuildParameter();
            queryBuilder.BuildProperty();
            queryBuilder.BuildExpression();
            queryBuilder.BuildMethodCall();

            foreach (var result in queryBuilder.GetResults())
            {
                CallChain.Enqueue(result);
            }
        }

        public IQueryable<TOut> Run<TIn, TOut> (IQueryable<TIn> source) where TIn : class where TOut : class
        {
            return (IQueryable<TOut>)Run(source, CallChain);
        }

        private IQueryable Run (IQueryable source, Queue<MethodCallExpression> expressions)
        {
            IQueryable NextInLine ()
            {
                return source.Provider.CreateQuery(UpdateCall(source, expressions.Dequeue()));
            }

            return expressions.Count > 1 ? Run(NextInLine(), expressions) : NextInLine();
        }

        private MethodCallExpression UpdateCall (IQueryable source, MethodCallExpression methodCall)
        {
            return methodCall.Update(null, new List<Expression> { Expression.Constant(source) }.Concat(StripParameters(methodCall)));
        }

        private IEnumerable<Expression> StripParameters (MethodCallExpression methodCall)
        {
            return methodCall.Arguments.Where(arg => arg.NodeType != ExpressionType.Parameter);
        }
    }
}
