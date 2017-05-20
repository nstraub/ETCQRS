using System;
using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryBuildDirector
    {
        IQueryBuildDirector And { get; }
        IQueryBuildDirector Or { get; }
        IQueryBuildDirector UsingType (Type type);
        IQueryBuildDirector Property (string propertyName);
        Expression<Func<T, bool>> GetResult<T> () where T : class;

        IQueryBuildDirector AddExpression (Func<Expression, Expression, BinaryExpression> operatorFunc, object value);

        IQueryBuildDirector Mutate ();
    }
}
