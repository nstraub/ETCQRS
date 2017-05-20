using ETCQRS.Query.Abstractions.Builder;


namespace ETCQRS.Query.Abstractions.Util
{
    public interface IExpressionMutator
    {
        void Execute (IQueryDescriptor context);
    }
}
