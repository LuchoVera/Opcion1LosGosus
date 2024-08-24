public class BookValidator : IValidator<Book> 
{
    private const int maxCharactersLength = 100;
    private const int minCharactersLength = 1;
    protected static bool ValidateTitle(string title) {
        return ValidateNullOrEmpty(title);
    }

    protected static bool ValidateAuthor(string author) {
        return ValidateNullOrEmpty(author);
    }
    protected static bool ValidateISBN(string isbn) {
        return !string.IsNullOrEmpty(isbn) && isbn.StartsWith("isbn-") && isbn.Length == 15 && isbn.Substring(5).All(char.IsDigit);
    }

    protected static bool ValidateGenre(string genre) {
        return ValidateNullOrEmpty(genre);
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
            { "Title", ValidateTitle(book.Title) },
            { "Author", ValidateAuthor(book.Author) },
            { "ISBN", ValidateISBN(book.ISBN) },
            { "Genre", ValidateGenre(book.Genre) },
            { "PublicationYear", ValidatePublicationYear(book.PublicationYear) }
        };
    }

    public bool Validate(Book book) {
        var validationResults = GetValidationResults(book);
        return validationResults.All(result => result.Value);
    }

}
