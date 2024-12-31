
using Agora.Common.Contracts;

namespace Agora.Common.Domain.Tests;

public class AggregateRootTests
{
    internal class TestAggregateRoot : AggregateRoot
    {
        public void RaiseTestEvent()
        {
            RaiseEvent(new TestDomainEvent());
        }
    }

    internal class TestDomainEvent : IDomainEvent;

    [Fact]
    public void ShouldReturnEmptyListWhenNoEvents()
    {
        var aggregateRoot = new TestAggregateRoot();

        Assert.Empty(aggregateRoot.DomainEvents);
    }

    [Fact]
    public void ShouldAddDomainEventWhenRaisingEvent()
    {
        var aggregateRoot = new TestAggregateRoot();

        aggregateRoot.RaiseTestEvent();

        Assert.NotEmpty(aggregateRoot.DomainEvents);
    }

    [Fact]
    public void ShouldClearDomainEventsWhenCleared()
    {
        var aggregateRoot = new TestAggregateRoot();
        aggregateRoot.RaiseTestEvent();

        aggregateRoot.ClearEvents();

        Assert.Empty(aggregateRoot.DomainEvents);
    }
}
