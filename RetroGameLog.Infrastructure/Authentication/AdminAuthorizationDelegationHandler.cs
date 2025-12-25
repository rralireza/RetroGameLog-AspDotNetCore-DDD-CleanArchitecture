using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using RetroGameLog.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace RetroGameLog.Infrastructure.Authentication;

public sealed class AdminAuthorizationDelegationHandler : DelegatingHandler
{
    private readonly KeyCloakOptions _keyCloakOptions;

    public AdminAuthorizationDelegationHandler(IOptions<KeyCloakOptions> keyCloakOptions)
    {
        _keyCloakOptions = keyCloakOptions.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requst, CancellationToken cancellationToken)
    {
        var authorizationToken = await GetAuthorizationToken(cancellationToken);

        requst.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, authorizationToken.AccessToken);

        HttpResponseMessage response = await base.SendAsync(requst, cancellationToken);

        return response;
    }

    private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authorizationRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keyCloakOptions.AdminClientId),
            new("client_secret", _keyCloakOptions.AdminClientSecret),
            new("scope","openid email"),
            new("grant_type", "client_credentials")
        };

        var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

        var authorizationRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_keyCloakOptions.TokenUrl))
        {
            Content = authorizationRequestContent
        };

        var authorizeResponse = await base.SendAsync(authorizationRequest, cancellationToken);

        authorizeResponse.EnsureSuccessStatusCode();

        return await authorizeResponse.Content.ReadFromJsonAsync<AuthorizationToken>() ?? throw new ApplicationException();
    }
}
