
using Agora.Common.Contracts;

namespace Agora.Common.Domain;

public interface IDomainEventPublisher
{
    void Publish<T>(T domainEvent) where T : IDomainEvent;
    Task PublishAsync<T>(T domainEvent) where T : IDomainEvent;
}
