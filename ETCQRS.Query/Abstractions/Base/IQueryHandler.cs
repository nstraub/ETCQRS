namespace ETCQRS.Query.Abstractions.Base
{
    public interface IQueryHandler<in TQuery> where TQuery : class, IQuery
    {
        void Handle (TQuery query);
    }
}
