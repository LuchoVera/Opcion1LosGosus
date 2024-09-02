namespace LMS.Business.Services.ErrorHandler.Exceptions;

public class InvalidInputException : Exception
{
    public InvalidInputException() {}

    public InvalidInputException(string message) : base(message) {}
}
