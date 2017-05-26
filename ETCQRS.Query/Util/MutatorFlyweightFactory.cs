using System;
using System.Collections.Generic;

using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.ExpressionOperatorMutators;


namespace ETCQRS.Query.Util
{
    public class MutatorFlyweightFactory : IFlyweightFactory<IExpressionMutator>
    {
        private readonly IReadOnlyDictionary<string, IExpressionMutator> _mutators;
        public MutatorFlyweightFactory ()
        {
            _mutators = new Dictionary<string, IExpressionMutator>(3)
                        {
                            { "Throwing", new ThrowingMutator() },
                            { "LessThan", new LessThanOrEqualMutator(this) },
                            { "GreaterThan", new GreaterThanOrEqualMutator(this) },
                            { "Null", new NullMutator() }
                        };
        }

        #region Implementation of IFlyweightFactory<out IExpressionMutator>

        public IExpressionMutator Get (string key)
        {
            try
            {
                return _mutators [key];
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException($"The given key, \"{key}\" was not present in the dictionary.", e);
            }
        }

        #endregion
    }
}
