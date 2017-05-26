using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Base;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryBuilder
    {
        void InitProperties (IQuery query);
        void Init (IQuery query);
        void BuildParameter ();
        void BuildProperty ();
        void BuildExpression ();
        void BuildMethodCall ();
        MethodCallExpression[] GetResults ();
    }
}
