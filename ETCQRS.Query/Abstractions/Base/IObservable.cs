namespace ETCQRS.Query.Abstractions.Base
{
	public interface IObservable
	{
		void Subscribe(params IObserver[] observers);
	}
}
