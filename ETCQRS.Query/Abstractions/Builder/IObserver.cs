using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IObserver
    {
        void Update (BinaryExpression expression);
    }
}
