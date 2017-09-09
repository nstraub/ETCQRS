namespace ETCQRS.Command.Abstractions
{
    // ReSharper disable once UnusedTypeParameter
    public abstract class CommandHandler<TCommand> : ICommandHandler where TCommand : class, ICommand
    {
        public abstract void Handle (ICommand command);
    }
}
