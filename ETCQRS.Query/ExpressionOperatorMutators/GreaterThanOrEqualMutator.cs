using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.ExpressionOperatorMutators
{
    public class GreaterThanOrEqualMutator : GenericMutatorTemplate
    {
        private readonly IFlyweightFactory<IExpressionMutator> _mutators;

        public GreaterThanOrEqualMutator (IFlyweightFactory<IExpressionMutator> mutators)
        {
            _mutators = mutators;
        }
        public override Func<Expression, Expression, BinaryExpression> GetTargetExpression()
        {
            return Expression.GreaterThanOrEqual;
        }

        public override IExpressionMutator NextMutator
        {
            get
            {
                return _mutators.Get("Null");
            }
        }
    }
}
