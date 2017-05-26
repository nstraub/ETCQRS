using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Extensions
{
    public static class QueryBuildDirectorExtensions
    {
        public static IQueryBuildFacade Equal (this IQueryBuildFacade builder, object value)
        {
            return builder.AddExpression(Expression.Equal, value);
        }

        public static IQueryBuildFacade NotEqual (this IQueryBuildFacade builder, object value)
        {
            return builder.AddExpression(Expression.NotEqual, value);
        }

        public static IQueryBuildFacade IsGreaterThan (this IQueryBuildFacade builder, object value)
        {
            builder.AddExpression(Expression.GreaterThan, value);
            return builder;
        }

        public static IQueryBuildFacade IsLessThan (this IQueryBuildFacade builder, object value)
        {
            builder.AddExpression(Expression.LessThan, value);
            return builder;
        }

        public static IQueryBuildFacade InOpenInterval (this IQueryBuildFacade builder, object leftEndpoint, object rightEndpoint)
        {
            return builder.IsGreaterThan(leftEndpoint).And.IsLessThan(rightEndpoint);
        }

        public static IQueryBuildFacade InClosedInterval (this IQueryBuildFacade builder, object leftEndpoint, object rightEndpoint)
        {
            return builder.IsGreaterThan(leftEndpoint).OrEqual().And.IsLessThan(rightEndpoint).OrEqual();
        }

        public static IQueryBuildFacade OrEqual (this IQueryBuildFacade builder)
        {
            return builder.Mutate();
        }
    }
}
