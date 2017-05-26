﻿using System;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Builder;


namespace ETCQRS.Query.Util
{
    public class QueryDescriptorFactory : IQueryDescriptorFactory
    {
        private readonly IFlyweightFactory<IExpressionMutator> _mutators;

        public QueryDescriptorFactory (IFlyweightFactory<IExpressionMutator> mutators)
        {
            _mutators = mutators;
        }

        public IQueryDescriptor Create (Type type)
        {
            return new QueryDescriptor(type, _mutators);
        }

        public IObserver CreateMutatorObserver (IQueryDescriptor descriptor)
        {
            return new MutatorObserver(descriptor, _mutators);
        }
    }
}
