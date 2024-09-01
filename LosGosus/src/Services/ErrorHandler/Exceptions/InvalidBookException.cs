namespace LosGosus.Services.ErrorHandler.Exceptions;

public class InvalidBookException : Exception
{
    public InvalidBookException() {}

    public InvalidBookException(string message) : base(message) {}
}
