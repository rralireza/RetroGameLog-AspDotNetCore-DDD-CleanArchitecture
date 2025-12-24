using RetroGameLog.Application.Abstractions.Authentication;
using RetroGameLog.Domain.Users;
using RetroGameLog.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace RetroGameLog.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public const string PasswordCredentialGrantType = "password";

    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var representation = UserRepresentationModel.FromUser(user);

        representation.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialGrantType
            }
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("users", representation, cancellationToken);

        return ExtractIdentityFromLocationHeader(response);
    }

    private static string ExtractIdentityFromLocationHeader(HttpResponseMessage response)
    {
        const string usersSegmentName = "users/";

        var locationHeader = response.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
            throw new InvalidOperationException("Location header can't be null");

        var usersSegmentValueIndex = locationHeader.IndexOf(usersSegmentName, StringComparison.OrdinalIgnoreCase);

        var userIdentityId = locationHeader.Substring(usersSegmentValueIndex + usersSegmentName.Length);

        return userIdentityId;
    }
}
