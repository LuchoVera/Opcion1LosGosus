public class InvalidBookException : Exception
{
    public InvalidBookException() {}

    public InvalidBookException(string message) : base(message) {}
}