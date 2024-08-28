public class BookValidator : IValidator<Book> 
{
    private const int maxCharactersLength = 100;
    private const int minCharactersLength = 1;

    protected static bool ValidateISBN(string isbn) {
        return !string.IsNullOrEmpty(isbn) && isbn.StartsWith("isbn-") && isbn.Length == 15 && isbn.Substring(5).All(char.IsDigit);
    }

    protected static bool ValidatePublicationYear(int publicationYear) {
        int currentYear = DateTime.Now.Year;
        return publicationYear > 0 && publicationYear <= currentYear;
    }

    protected static bool ValidateNullOrEmpty(string value)
    {
        return !string.IsNullOrEmpty(value) && value.Length <= maxCharactersLength && value.Length >= minCharactersLength;
    }

    public Dictionary<string, bool> GetValidationResults(Book book) {
        return new Dictionary<string, bool>
        {
            { "Title", ValidateNullOrEmpty(book.Title) },
            { "Author", ValidateNullOrEmpty(book.Author) },
            { "ISBN", ValidateISBN(book.ISBN) },
            { "Genre", ValidateNullOrEmpty(book.Genre) },
            { "PublicationYear", ValidatePublicationYear(book.PublicationYear) }
        };
    }

    public bool Validate(Book book) {
        var validationResults = GetValidationResults(book);
        return validationResults.All(result => result.Value);
    }
}
