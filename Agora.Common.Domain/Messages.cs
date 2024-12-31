
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Agora.Common.Domain;

public sealed class Messages : ICommandDispatcher, IQueryDispatcher
{
    private readonly IServiceProvider _provider;

    public Messages(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task<Result> Dispatch<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = _provider.GetRequiredService<ICommandHandler<TCommand>>();
        return await handler.Handle(command);
    }

    public async Task<Result<TResult>> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
    {
        var handler = _provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query);
    }
}
