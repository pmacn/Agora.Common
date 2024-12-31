using CSharpFunctionalExtensions;

namespace Agora.Common.Domain;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<Result> Handle(TCommand command);
}
