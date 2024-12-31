namespace Agora.Common.Contracts;

/// <summary>
/// Represents the marker interface for a domain event.
/// Domain events are events that are inherently a result of a business operation 
/// and are part of the domain model.
/// </summary>
/// <remarks>
/// Implementing this interface in a class marks it as a domain event.
/// Such events can be used to trigger various domain-specific actions and workflows.
/// </remarks>
public interface IDomainEvent;