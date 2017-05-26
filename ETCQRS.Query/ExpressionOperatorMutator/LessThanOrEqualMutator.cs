using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.ExpressionOperatorMutator
{
    public class LessThanOrEqualMutator : GenericMutatorTemplate
    {
        private readonly IFlyweightFactory<IExpressionMutator> _mutators;

        public LessThanOrEqualMutator (IFlyweightFactory<IExpressionMutator> mutators)
        {
            _mutators = mutators;
        }
        public override Func<Expression, Expression, BinaryExpression> GetTargetExpression()
        {
            return Expression.LessThanOrEqual;
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
