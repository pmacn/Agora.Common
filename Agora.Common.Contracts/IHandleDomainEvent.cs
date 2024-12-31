
namespace Agora.Common.Contracts;
public interface IHandleDomainEvent<T> where T : IDomainEvent
{
    Task Handle(T domainEvent);
}

