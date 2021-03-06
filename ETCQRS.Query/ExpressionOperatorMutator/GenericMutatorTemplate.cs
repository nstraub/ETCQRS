﻿using EnsureThat;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Resources;
using System;
using System.Linq.Expressions;

namespace ETCQRS.Query.ExpressionOperatorMutator
{
	public abstract class GenericMutatorTemplate : IExpressionMutator
	{
		public virtual void Execute(IQueryDescriptor context)
		{
			Ensure.That(context.Query).WithException(e => new NullReferenceException(ErrorMessages.QueryNullReference)).IsNotNull();
			Ensure.That(NextMutator).WithException(e => new NullReferenceException(ErrorMessages.NextMutatorNullReference)).IsNotNull();

			var nodeType = context.Query.NodeType;
			var expression = GetTargetExpression();

			if (expression != null)
			{
				if (nodeType == ExpressionType.AndAlso || nodeType == ExpressionType.OrElse)
				{
					var expressionToRecreate = (BinaryExpression)context.Query.Right;

					context.Query = (BinaryExpression)context.Query.Left;

					var joinExpression = nodeType == ExpressionType.AndAlso ?
											 (Func<Expression, Expression, BinaryExpression>)Expression.AndAlso :
											 Expression.OrElse;

					context.Query = joinExpression(context.Query, expression(expressionToRecreate.Left, expressionToRecreate.Right));
				}
				else
				{
					context.Query = expression(context.Query.Left, context.Query.Right);
				}
			}
			context.SetMutator(NextMutator);
		}

		public abstract Func<Expression, Expression, BinaryExpression> GetTargetExpression();

		public abstract IExpressionMutator NextMutator { get; }
	}
}
