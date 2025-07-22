/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/



/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


using Microsoft.EntityFrameworkCore;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class IdentityRepository(
    UserAppContext _context
) : IIdentityRepository
{
    public Task<UserIdentity?> GetByProviderIdAsync(string providerId)
    {
        return _context.UserIdentities
            .Include(i => i.User)
            .Where(x => x.ProviderId == providerId)
            .FirstOrDefaultAsync();
    }
}