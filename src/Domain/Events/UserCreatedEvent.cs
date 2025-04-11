/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using Steve.ManagerHero.UserService.Domain.Common;

public sealed record UserCreatedEvent(User User) : IDomainEvent;