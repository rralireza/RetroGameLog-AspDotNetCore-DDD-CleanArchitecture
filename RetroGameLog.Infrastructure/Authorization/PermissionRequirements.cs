using Microsoft.AspNetCore.Authorization;

namespace RetroGameLog.Infrastructure.Authorization;

internal sealed class PermissionRequirements : IAuthorizationRequirement
{
    public PermissionRequirements(string permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}
