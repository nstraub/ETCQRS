using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.ExpressionOperatorMutator
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

        public override IExpressionMutator NextMutator => _mutators.Get("Null");
    }
}
