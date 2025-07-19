/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/


public sealed record EmailVerificationEvent(User User) : IDomainEvent;