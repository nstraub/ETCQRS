using System;

using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Abstractions.Util
{
    public interface IQueryDescriptorFactory
    {
        IQueryDescriptor Create (Type type);
    }
}
