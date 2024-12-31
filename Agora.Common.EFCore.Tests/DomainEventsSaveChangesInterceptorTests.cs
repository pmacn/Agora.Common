using Agora.Common.Contracts;
using Agora.Common.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Agora.Common.EFCore.Tests;

public class DomainEventsSaveChangesInterceptorTests
{
    internal class TestDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestAggregateRoot>();
        }
    }

    internal class TestDomainEvent : IDomainEvent;

    internal class TestAggregateRoot : AggregateRoot
    {
        public void RaiseEvent()
        {
            RaiseEvent(new TestDomainEvent());
        }
    }

    [Fact]
    public async Task ShouldPublishDomainEventsWhenSavingChanges()
    {
        var domainEventPublisherMock = new Mock<IDomainEventPublisher>();
        var domainEventsSaveChangesInterceptor = new DomainEventsSaveChangesInterceptor(
            domainEventPublisherMock.Object,
            Mock.Of<ILogger<DomainEventsSaveChangesInterceptor>>());
        var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .AddInterceptors(domainEventsSaveChangesInterceptor)
            .Options;

        var dbContext = new TestDbContext(dbContextOptions);
        var aggregateRoot = new TestAggregateRoot();
        dbContext.Add(aggregateRoot);
        aggregateRoot.RaiseEvent();

        await dbContext.SaveChangesAsync();
        domainEventPublisherMock.Verify(x => x.PublishAsync<IDomainEvent>(It.IsAny<TestDomainEvent>()), Times.Once);
    }

    [Fact]
    public async Task ShouldPublishDomainEventsWhenSavingChangesAsync()
    {
        var domainEventPublisherMock = new Mock<IDomainEventPublisher>();
        var domainEventsSaveChangesInterceptor = new DomainEventsSaveChangesInterceptor(
                       domainEventPublisherMock.Object,
                                  Mock.Of<ILogger<DomainEventsSaveChangesInterceptor>>());
        var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .AddInterceptors(domainEventsSaveChangesInterceptor)
            .Options;
        var dbContext = new TestDbContext(dbContextOptions);
        var aggregateRoot = new TestAggregateRoot();
        dbContext.Add(aggregateRoot);
        aggregateRoot.RaiseEvent();

        await dbContext.SaveChangesAsync();

        domainEventPublisherMock.Verify(x => x.PublishAsync<IDomainEvent>(It.IsAny<TestDomainEvent>()), Times.Once);
    }
}
