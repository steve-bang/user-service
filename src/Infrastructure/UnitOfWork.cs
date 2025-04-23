/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Domain.Common;
using Steve.ManagerHero.UserService.Infrastructure;
using Steve.ManagerHero.UserService.Infrastructure.Repository;

public class UnitOfWork( UserAppContext _context ) : IUnitOfWork
{
    private IUserRepository? _userRepository;
    private IRoleRepository? _roleRepository;
    private ISessionRepository? _sessionRepository;
    private IPermissionRepository? _permissionRepository;


    public IUserRepository Users => _userRepository ??= new UserRepository(_context);
    public IRoleRepository Roles => _roleRepository ??= new RoleRepository(_context);
    public ISessionRepository Sessions => _sessionRepository ??= new SessionRepository(_context);
    public IPermissionRepository Permissions => _permissionRepository ??= new PermissionRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var result = await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        });
    }

    public void Dispose() => _context.Dispose();
}