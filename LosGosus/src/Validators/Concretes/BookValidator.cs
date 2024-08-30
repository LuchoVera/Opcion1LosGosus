public sealed class BookValidator() : BaseValidator<Book>(100)
{
    private bool ValidateTitle(string title)
    {
        return ValidateNotNullOrEmpty(title)
            && ValidateLength(title)
            && title.All(static c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
    }

    private bool ValidateAuthor(string author)
    {
        return ValidateNotNullOrEmpty(author)
            && ValidateLength(author)
            && ValidateStringLettersWithSpaces(author);
    }

    private bool ValidateISBN(string isbn)
    {
        return ValidateNotNullOrEmpty(isbn)
            && isbn.StartsWith("isbn-", StringComparison.Ordinal)
            && isbn.Length == 15
            && isbn[5..].All(char.IsDigit);
    }

    private bool ValidateGenre(string genre)
    {
        return ValidateNotNullOrEmpty(genre)
            && ValidateLength(genre)
            && genre.All(static c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-');
    }

    private bool ValidatePublicationYear(int publicationYear)
    {
        int currentYear = DateTime.Now.Year;
        return publicationYear > 0 && publicationYear <= currentYear;
    }

    protected override IList<Func<Book, bool>> ValidationResults()
    {
        return
        [
            book => ValidateTitle(book.Title),
            book => ValidateAuthor(book.Author),
            book => ValidateISBN(book.ISBN),
            book => ValidateGenre(book.Genre),
            book => ValidatePublicationYear(book.PublicationYear),
        ];
    }

    public new bool Validate(Book book) {
        var validationResults = ValidationResults();
        return validationResults.All(func => func(book));
    }
}
