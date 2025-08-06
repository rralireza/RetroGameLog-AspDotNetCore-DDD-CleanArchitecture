namespace RetroGameLog.Application.Abstractions.Exceptions;

public sealed class ValidationException : Exception
{
    public IEnumerable<ValidationErrors> Errors { get; }

    public ValidationException(IEnumerable<ValidationErrors> errors)
    {
        Errors = errors;
    }
}
