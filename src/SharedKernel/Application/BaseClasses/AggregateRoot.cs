/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.SharedKernel.Application.BaseClasses;

public abstract class AggregateRoot : Entity
{
    /// <summary>
    /// The events that have been applied to the aggregate root.
    /// </summary>
    private readonly List<IDomainEvent> _events = new();

    /// <summary>
    /// The events that have been applied to the aggregate root.
    /// </summary>
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    public AggregateRoot()
    {
        Id = CreateNewId();
    }

    /// <summary>
    /// Clear the events that have been applied to the aggregate root.
    /// </summary>
    public void ClearEvents()
    {
        _events.Clear();
    }

    /// <summary>
    /// Add a domain event to the aggregate root.
    /// </summary>
    /// <param name="domainEvent"></param>
    public void AddEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }
}