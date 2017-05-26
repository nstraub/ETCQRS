using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Builder
{
    public class QueryDescriptor : IQueryDescriptor
    {
        public QueryDescriptor (Type type, IFlyweightFactory<IExpressionMutator> mutators)
        {
            Parameter = Expression.Parameter(type);
            Mutator = mutators.Get("Throwing");
        }

        private IExpressionMutator Mutator { get; set; }
        public void SetMutator (IExpressionMutator mutator)
        {
            Mutator = mutator;
        }

        public void Mutate ()
        {
            Mutator.Execute(this);
        }

        public virtual BinaryExpression Query { get; set; }
        public ParameterExpression Parameter { get; }
        public MemberExpression PropertyExpression { get; set; }
    }
}
