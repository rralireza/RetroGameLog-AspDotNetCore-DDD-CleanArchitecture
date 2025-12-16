namespace RetroGameLog.Infrastructure.Authentication;

public sealed class AuthenticationOptions
{
    public string Audience { get; init; } = string.Empty;

    public string MetaDataUrl { get; init;  } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public bool RequireHttpsMetadata { get; init; }
}
