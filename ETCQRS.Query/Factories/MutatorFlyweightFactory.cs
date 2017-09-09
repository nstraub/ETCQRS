using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutator;
using System.Collections.Generic;

namespace ETCQRS.Query.Factories
{
	public class MutatorFlyweightFactory : IFlyweightFactory<IExpressionMutator>
	{
		private readonly IReadOnlyDictionary<string, IExpressionMutator> _mutators;

		public MutatorFlyweightFactory()
		{
			_mutators = new Dictionary<string, IExpressionMutator>(3)
						{
							{ "Throwing", new ThrowingMutator() },
							{ "LessThan", new LessThanOrEqualMutator(this) },
							{ "GreaterThan", new GreaterThanOrEqualMutator(this) },
							{ "Null", new NullMutator() }
						};
		}

		public virtual IExpressionMutator Get(string key)
		{
			try
			{
				return _mutators[key];
			}
			catch (KeyNotFoundException e)
			{
				throw new KeyNotFoundException($"The given key, \"{key}\" is not present in the dictionary.", e);
			}
		}
	}
}
