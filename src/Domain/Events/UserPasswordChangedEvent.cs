/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Events;

public sealed record UserPasswordChangedEvent(User User) : IDomainEvent;