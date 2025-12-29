using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RetroGameLog.Infrastructure.Authorization;

internal sealed class CustomClaimTransformation : IClaimsTransformation
{

    private readonly IServiceProvider _serviceProvider;

    public CustomClaimTransformation(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(x => x.Type == ClaimTypes.Role) && principal.HasClaim(x => x.Type == JwtRegisteredClaimNames.Sub))
        {
            return principal;
        }

        using var scope = _serviceProvider.CreateScope();

        var authorizationService = scope
            .ServiceProvider
            .GetRequiredService<AuthorizationService>();

        var identityId = principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ApplicationException("User idenetity not found!");

        var userRoles = await authorizationService
            .GetRolesForUserAsync(identityId);

        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userRoles.UserId.ToString()));

        foreach (var role in userRoles.Roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
