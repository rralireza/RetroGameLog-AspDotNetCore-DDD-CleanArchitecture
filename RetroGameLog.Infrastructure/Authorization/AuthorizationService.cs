using Microsoft.EntityFrameworkCore;
using RetroGameLog.Domain.Users;
using RetroGameLog.Infrastructure.DatabaseContext;

namespace RetroGameLog.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly RetroGameLogDbContext _context;

    public AuthorizationService(RetroGameLogDbContext context)
    {
        _context = context;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var roles = await _context
            .Set<User>()
            .Where(x => x.IdentityId == identityId)
            .Select(x => new UserRolesResponse
            {
                UserId = x.Id,
                Roles = x.Roles.ToList()
            }).FirstOrDefaultAsync() ?? throw new ApplicationException();

        return roles;
    }
}
