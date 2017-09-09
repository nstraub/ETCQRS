using System;

namespace ETCQRS.Query.Abstractions.Base
{
	public interface IQuery
	{
		Type ParameterType { get; }
		string PropertyName { get; }
	}
}
