using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Builder {
    public class MutatorObserver : IObserver
    {
        private readonly IQueryDescriptor _descriptor;
        private readonly IFlyweightFactory<IExpressionMutator> _mutators;

        public MutatorObserver (IQueryDescriptor descriptor, IFlyweightFactory<IExpressionMutator> mutators)
        {
            _descriptor = descriptor;
            _mutators = mutators;
        }


        public void Update (BinaryExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}