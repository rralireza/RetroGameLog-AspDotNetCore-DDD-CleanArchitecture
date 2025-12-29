using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RetroGameLog.Infrastructure.Authorization;

internal sealed class CustomClaimTransformation : IClaimsTransformation
{


    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(x => x.Type == ClaimTypes.Role) && principal.HasClaim(x => x.Type == JwtRegisteredClaimNames.Sub))
        {
            return principal;
        }


    }
}
