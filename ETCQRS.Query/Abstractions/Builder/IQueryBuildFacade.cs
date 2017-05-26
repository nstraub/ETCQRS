using System;
using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryBuildFacade
    {
        IQueryBuildFacade And { get; }
        IQueryBuildFacade Or { get; }
        IQueryBuildFacade UsingType (Type type);
        IQueryBuildFacade Property (string propertyName);

        IQueryBuildFacade AddExpression (Func<Expression, Expression, BinaryExpression> operatorFunc, object value);

        Expression<Func<T, bool>> GetResult<T> () where T : class;

        IQueryBuildFacade Mutate ();

        IQueryComposite Composite { get; }
    }
}
