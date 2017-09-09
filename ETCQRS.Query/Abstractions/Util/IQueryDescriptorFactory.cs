using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using System;

namespace ETCQRS.Query.Abstractions.Util
{
	public interface IQueryDescriptorFactory
	{
		IQueryDescriptor Create(Type type);

		IObserver CreateMutatorObserver(IQueryDescriptor descriptor);
	}
}
