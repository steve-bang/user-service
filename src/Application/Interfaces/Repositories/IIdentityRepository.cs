/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface IIdentityRepository : IRepository
{
    Task<UserIdentity?> GetByProviderIdAsync(string providerId);
}