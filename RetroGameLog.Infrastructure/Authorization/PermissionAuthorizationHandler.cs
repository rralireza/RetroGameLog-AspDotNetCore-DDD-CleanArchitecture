using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace RetroGameLog.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirements>
{
    private readonly IServiceProvider _serivceProvider;

    public PermissionAuthorizationHandler(IServiceProvider serivceProvider)
    {
        _serivceProvider = serivceProvider;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirements requirement)
    {
        if (context.User.Identity is not { IsAuthenticated: true })
            return;

        using var scope = _serivceProvider.CreateScope();

        var authorizationService = scope.ServiceProvider.GetRequiredService<AuthorizationService>();

        var identityId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ApplicationException("User identity not found!");

        var permissions = await authorizationService.GetPermissionsForUserAsync(identityId);

        if (permissions.Contains(requirement.Permission))
            context.Succeed(requirement);
    }
}
