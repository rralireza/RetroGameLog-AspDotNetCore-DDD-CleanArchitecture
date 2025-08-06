namespace RetroGameLog.Application.Abstractions.Exceptions;

public sealed record ValidationErrors(string Code, string Name, string Message);

