using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Util;


namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IQueryDescriptor
    {
        BinaryExpression Query { get; set; }
        ParameterExpression Parameter { get; }
        MemberExpression PropertyExpression { get; set; }
        IExpressionMutator Mutator { get; }

        void SetMutator (IExpressionMutator mutator);

        void Mutate ();
    }
}
