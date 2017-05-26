using System.Linq.Expressions;


namespace ETCQRS.Query.Abstractions.Base
{
    public interface IObserver
    {
        void Update (BinaryExpression expression);
    }
}
