namespace LosGosus.Services.ErrorHandler.Exceptions;

public class InvalidPatronException : Exception
{
    public InvalidPatronException() {}

    public InvalidPatronException(string message) : base(message) {}
}
