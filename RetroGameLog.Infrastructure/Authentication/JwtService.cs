using Microsoft.Extensions.Options;
using RetroGameLog.Application.Abstractions.Authentication;
using RetroGameLog.Domain.Abstractions;
using RetroGameLog.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace RetroGameLog.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    private readonly HttpClient _httpClient;
    private readonly KeyCloakOptions _keyCloakOptions;

    public JwtService(HttpClient httpClient, IOptions<KeyCloakOptions> keyCloakOptions)
    {
        _httpClient = httpClient;
        _keyCloakOptions = keyCloakOptions.Value;
    }

    public async Task<Result<string>> GenerateTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var loginRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keyCloakOptions.AdminClientId),
            new("client_secret", _keyCloakOptions.AdminClientSecret),
            new("scope", "openid email"),
            new("grant_type", "password"),
            new("username", email),
            new("password", password)
        };

        FormUrlEncodedContent loginContent = new(loginRequestParameters);

        HttpResponseMessage response = await _httpClient.PostAsync("", loginContent, cancellationToken);

        response.EnsureSuccessStatusCode();

        AuthorizationToken? token = await response.Content.ReadFromJsonAsync<AuthorizationToken>();

        if (token is null)
            return Result.Failure<string>(Error.NotFound);

        return token.AccessToken;
    }
}
