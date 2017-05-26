namespace ETCQRS.Query.Abstractions.Builder
{
    public interface IObservable
    {
        void Subscribe (params IObserver[] observers);
    }
}
