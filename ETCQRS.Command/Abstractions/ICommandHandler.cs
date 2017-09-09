namespace ETCQRS.Command.Abstractions
{
    public interface ICommandHandler
    {
        void Handle (ICommand command);
    }
}
