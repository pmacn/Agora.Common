
using CSharpFunctionalExtensions;

namespace Agora.Common.Domain;
public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<Result<TResult>> Handle(TQuery query);
}
