/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Common;

public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}