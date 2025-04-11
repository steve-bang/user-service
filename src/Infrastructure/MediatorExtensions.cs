/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Infrastructure;

static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, UserAppContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}