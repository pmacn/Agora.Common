
using CSharpFunctionalExtensions;

namespace Agora.Common.Domain;

public interface ICommandDispatcher
{
    Task<Result> Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
}
