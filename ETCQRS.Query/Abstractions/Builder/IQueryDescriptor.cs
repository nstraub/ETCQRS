﻿using ETCQRS.Query.Abstractions.Util;
using System.Linq.Expressions;

namespace ETCQRS.Query.Abstractions.Builder
{
	public interface IQueryDescriptor
	{
		BinaryExpression Query { get; set; }
		ParameterExpression Parameter { get; }
		MemberExpression Property { get; set; }

		void SetMutator(IExpressionMutator mutator);

		void Mutate();
	}
}
