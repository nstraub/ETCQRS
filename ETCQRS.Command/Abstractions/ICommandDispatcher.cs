namespace ETCQRS.Command.Abstractions
{
    public interface ICommandDispatcher
    {
        void Dispatch (params ICommand[] commands);
        void Save ();
    }
}
