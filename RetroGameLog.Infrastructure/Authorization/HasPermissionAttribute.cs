using Microsoft.AspNetCore.Authorization;

namespace RetroGameLog.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
        
    }
}
