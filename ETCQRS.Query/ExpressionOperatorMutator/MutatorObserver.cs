using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using System.Linq.Expressions;

namespace ETCQRS.Query.ExpressionOperatorMutator
{
	public class MutatorObserver : IObserver
	{
		private readonly IQueryDescriptor _descriptor;
		private readonly IFlyweightFactory<IExpressionMutator> _mutators;

		public MutatorObserver(IQueryDescriptor descriptor, IFlyweightFactory<IExpressionMutator> mutators)
		{
			_descriptor = descriptor;
			_mutators = mutators;
		}

		public void Update(BinaryExpression expression)
		{
			switch (expression.NodeType)
			{
				case ExpressionType.GreaterThan:
					_descriptor.SetMutator(_mutators.Get("GreaterThan"));
					break;

				case ExpressionType.LessThan:
					_descriptor.SetMutator(_mutators.Get("LessThan"));
					break;

				default:
					_descriptor.SetMutator(_mutators.Get("Null"));
					break;
			}
		}
	}
}
