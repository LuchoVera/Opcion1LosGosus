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

    public bool Validate(Book book) {
        return ValidateTitle(book.Title) &&
               ValidateAuthor(book.Author) &&
               ValidateISBN(book.ISBN) &&
               ValidateGenre(book.Genre) &&
               ValidatePublicationYear(book.PublicationYear);
    }

}
