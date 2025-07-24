namespace RetroGameLog.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new(string.Empty, string.Empty);

    public static Error NotFound = new("Error.NotFound", "Not Found");

    public static Error InvalidInput = new("Error.InvalidInput", "Invalid Input");

    public static Error Unauthorized = new("Error.Unauthorized", "Unauthorized");

    public static Error InternalServerError = new("Error.InternalServerError", "Internal Server Error");

    public static Error Conflict = new("Error.Conflict", "Conflict");

    public static Error BadRequest = new("Error.BadRequest", "Bad Request");

    public static Error Forbidden = new("Error.Forbidden", "Forbidden");

    public static Error UnprocessableEntity = new("Error.UnprocessableEntity", "Unprocessable Entity");

    public static Error ServiceUnavailable = new("Error.ServiceUnavailable", "Service Unavailable");

    public static Error NotImplemented = new("Error.NotImplemented", "Not Implemented");

    public static Error TooManyRequests = new("Error.TooManyRequests", "Too Many Requests");

    public static Error GatewayTimeout = new("Error.GatewayTimeout", "Gateway Timeout");

    public static Error BadGateway = new("Error.BadGateway", "Bad Gateway");

    public static Error UnsupportedMediaType = new("Error.UnsupportedMediaType", "Unsupported Media Type");

    public static Error MethodNotAllowed = new("Error.MethodNotAllowed", "Method Not Allowed");

    public static Error NullValue = new("Error.NullValue", "Null Value");
}
