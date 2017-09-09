using ETCQRS.Query.Abstractions.Builder;
using System.Linq.Expressions;

namespace ETCQRS.Query.Extensions
{
	public static class QueryBuildDirectorExtensions
	{
		public static IQueryExpressionBuilder Equal(this IQueryExpressionBuilder builder, object value)
		{
			return builder.AddExpression(Expression.Equal, value);
		}

		public static IQueryExpressionBuilder NotEqual(this IQueryExpressionBuilder builder, object value)
		{
			return builder.AddExpression(Expression.NotEqual, value);
		}

		public static IQueryExpressionBuilder IsGreaterThan(this IQueryExpressionBuilder builder, object value)
		{
			builder.AddExpression(Expression.GreaterThan, value);
			return builder;
		}

		public static IQueryExpressionBuilder IsLessThan(this IQueryExpressionBuilder builder, object value)
		{
			builder.AddExpression(Expression.LessThan, value);
			return builder;
		}

		public static IQueryExpressionBuilder InOpenInterval(this IQueryExpressionBuilder builder, object leftEndpoint, object rightEndpoint)
		{
			return builder.IsGreaterThan(leftEndpoint).And.IsLessThan(rightEndpoint);
		}

		public static IQueryExpressionBuilder InClosedInterval(this IQueryExpressionBuilder builder, object leftEndpoint, object rightEndpoint)
		{
			return builder.IsGreaterThan(leftEndpoint).OrEqual().And.IsLessThan(rightEndpoint).OrEqual();
		}

		public static IQueryExpressionBuilder OrEqual(this IQueryExpressionBuilder builder)
		{
			return builder.Mutate();
		}
	}
}
