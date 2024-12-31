
using CSharpFunctionalExtensions;

namespace Agora.Common.Domain;

public interface IQueryDispatcher
{
    Task<Result<TResult>> Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}
