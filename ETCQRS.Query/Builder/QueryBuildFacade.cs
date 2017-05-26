using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Builder
{
    public class QueryBuildFacade : IQueryBuildFacade
    {
        public Queue<MethodCallExpression> CallChain { get; }
        public QueryBuildFacade ()
        {
            CallChain = new Queue<MethodCallExpression>();
        }


        public void AddQuery (IQuery query, IQueryBuilder queryBuilder)
        {
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
                return source.Provider.CreateQuery(UpdateCall(source, CallChain.Dequeue()));
            }

            return expressions.Count > 1 ? Run(NextInLine(), CallChain) : NextInLine();
        }

        private MethodCallExpression UpdateCall (IQueryable source, MethodCallExpression methodCallExpression)
        {
            return methodCallExpression.Update(null, new List<Expression>
                                                     {
                                                         Expression.Constant(source)
                                                     }.Concat(StripParameters(methodCallExpression)));
        }

        private IEnumerable<Expression> StripParameters (MethodCallExpression methodCallExpression)
        {
            return methodCallExpression.Arguments.Where(arg => arg.NodeType != ExpressionType.Parameter);
        }
    }
}
