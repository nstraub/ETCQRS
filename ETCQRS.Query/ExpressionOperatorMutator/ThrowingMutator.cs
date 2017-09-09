using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;

namespace ETCQRS.Query.ExpressionOperatorMutator
{
	public class ThrowingMutator : IExpressionMutator
	{
		public void Execute(IQueryDescriptor context)
		{
			throw new System.InvalidOperationException();
		}
	}
}
