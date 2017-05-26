using System;
using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Extensions
{
    public static class QueryBuildDirectorExtensions
    {
        public static IQueryBuildDirector Equal (this IQueryBuildDirector builder, object value)
        {
            return builder.AddExpression(Expression.Equal, value);
        }

        public static IQueryBuildDirector NotEqual (this IQueryBuildDirector builder, object value)
        {
            return builder.AddExpression(Expression.NotEqual, value);
        }

        public static IQueryBuildDirector IsGreaterThan (this IQueryBuildDirector builder, object value)
        {
            builder.AddExpression(Expression.GreaterThan, value);
            return builder;
        }

        public static IQueryBuildDirector IsLessThan (this IQueryBuildDirector builder, object value)
        {
            builder.AddExpression(Expression.LessThan, value);
            return builder;
        }

        public static IQueryBuildDirector InOpenInterval (this IQueryBuildDirector builder, object leftEndpoint, object rightEndpoint)
        {
            return builder.IsGreaterThan(leftEndpoint).And.IsLessThan(rightEndpoint);
        }

        public static IQueryBuildDirector InClosedInterval (this IQueryBuildDirector builder, object leftEndpoint, object rightEndpoint)
        {
            return builder.IsGreaterThan(leftEndpoint).OrEqual().And.IsLessThan(rightEndpoint).OrEqual();
        }

        public static IQueryBuildDirector OrEqual (this IQueryBuildDirector builder)
        {
            return builder.Mutate();
        }
    }
}
