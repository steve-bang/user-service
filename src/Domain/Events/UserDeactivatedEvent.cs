/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Domain.Events;

public sealed record UserDeactivatedEvent(User User) : IDomainEvent;