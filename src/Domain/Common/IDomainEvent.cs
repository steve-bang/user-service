/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using MediatR;

namespace Steve.ManagerHero.UserService.Domain.Common;

/// <summary>
/// Interface for a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
}
