using Agora.Common.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Agora.Common.EFCore;

/// <summary>
/// Interceptor for Entity Framework SaveChanges operations that publishes domain events after entities are persisted.
/// </summary>
public class DomainEventsSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventPublisher _domainEventPublisher;
    private readonly ILogger<DomainEventsSaveChangesInterceptor> _logger;

    /// <summary>
    /// Initializes a new instance of the DomainEventsSaveChangesInterceptor class.
    /// </summary>
    /// <param name="domainEventPublisher">The publisher to use for dispatching domain events.</param>
    /// <param name="logger">Logger for logging the save changes and domain event publication.</param>
    public DomainEventsSaveChangesInterceptor(
        IDomainEventPublisher domainEventPublisher,
        ILogger<DomainEventsSaveChangesInterceptor> logger)
    {
        _domainEventPublisher = domainEventPublisher;
        _logger = logger;
    }

    /// <summary>
    /// Overrides the SavedChanges method to publish domain events after changes are saved.
    /// </summary>
    /// <param name="eventData">Event data containing the DbContext used for the operation.</param>
    /// <param name="result">The number of state entries written to the database.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        _logger.LogInformation("DomainEventsSaveChangesInterceptor.SavedChanges");
        PublishDomainEvents(eventData);

        return base.SavedChanges(eventData, result);
    }

    /// <summary>
    /// Asynchronously overrides the SavedChanges method to publish domain events after changes are saved.
    /// </summary>
    /// <param name="eventData">Event data containing the DbContext used for the operation.</param>
    /// <param name="result">The number of state entries written to the database.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("DomainEventsSaveChangesInterceptor.SavedChangesAsync");
        await PublishDomainEventsAsync(eventData);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(SaveChangesCompletedEventData eventData)
    {
        if (eventData.Context is not null)
        {
            var domainEvents = eventData.Context.ChangeTracker
            .Entries()
            .Select(x => x.Entity)
            .OfType<AggregateRoot>()
            .SelectMany(x => x.DomainEvents)
            .ToList();

            await Task.WhenAll(domainEvents.Select(_domainEventPublisher.PublishAsync));
        }
    }

    private void PublishDomainEvents(SaveChangesCompletedEventData eventData)
    {
        if (eventData.Context is not null)
        {
            var domainEvents = eventData.Context.ChangeTracker
            .Entries()
            .Select(x => x.Entity)
            .OfType<AggregateRoot>()
            .SelectMany(x => x.DomainEvents)
            .ToList();

            foreach (var domainEvent in domainEvents)
            {
                _domainEventPublisher.Publish(domainEvent);
            }
        }
    }
}
