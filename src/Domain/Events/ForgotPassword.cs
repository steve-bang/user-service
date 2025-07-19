/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Domain.Events;

public sealed record ForgotPasswordEvent(User User) : IDomainEvent;