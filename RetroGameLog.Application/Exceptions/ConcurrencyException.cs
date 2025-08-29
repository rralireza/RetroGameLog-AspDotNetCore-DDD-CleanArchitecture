namespace RetroGameLog.Application.Exceptions;

public sealed class ConcurrencyException : Exception
{
    public ConcurrencyException(string message, Exception ex) : base(message, ex)
    {
    }
}
