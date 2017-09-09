using ETCQRS.Query.Abstractions.Util;
using System;
using System.Linq.Expressions;

namespace ETCQRS.Query.ExpressionOperatorMutator
{
	public class LessThanOrEqualMutator : GenericMutatorTemplate
	{
		private readonly IFlyweightFactory<IExpressionMutator> _mutators;

		public LessThanOrEqualMutator(IFlyweightFactory<IExpressionMutator> mutators)
		{
			_mutators = mutators;
		}

		public override Func<Expression, Expression, BinaryExpression> GetTargetExpression()
		{
			return Expression.LessThanOrEqual;
		}

		public override IExpressionMutator NextMutator => _mutators.Get("Null");
	}
}
