using LosGosus.Models;
using LosGosus.Validators.Bases;

namespace LosGosus.Validators.Concretes;

public sealed class BookValidator() : BaseValidator<Book>(100)
{

    private bool ValidateTitle(string title)
    {
        return !string.IsNullOrEmpty(title) 
            && title.Length <= MaxCharactersLength 
            && title.Length >= MinCharactersLength;
    }

    private bool ValidateAuthor(string author)
    {
        return ValidateNotNullOrEmpty(author)
            && ValidateLength(author)
            && ValidateStringLettersWithSpaces(author);
    }

    private bool ValidateISBN(string isbn) {
        return !string.IsNullOrEmpty(isbn) 
            && isbn.StartsWith("isbn-") 
            && isbn.Length == 15 
            && isbn.Substring(5).All(char.IsDigit);
    }

    private bool ValidateGenre(string genre)
    {
        return ValidateNotNullOrEmpty(genre)
            && ValidateLength(genre)
            && ValidateStringLettersWithSpaces(genre);
    }
    
    private bool ValidatePublicationYear(int publicationYear) {
        int currentYear = DateTime.Now.Year;
        return publicationYear > 0 && publicationYear <= currentYear;
    }

    protected override IList<Func<Book, bool>> ValidationResults()
    {
        return new List<Func<Book, bool>>
        {
            book => ValidateTitle(book.Title),
            book => ValidateAuthor(book.Author),
            book => ValidateISBN(book.ISBN),
            book => ValidateGenre(book.Genre),
            book => ValidatePublicationYear(book.PublicationYear),
        };
    }

    public new bool Validate(Book book) {
        foreach (Func<Book, bool> rule in ValidationResults())
        {
            if (!rule(book))
            {
                return false;
            }
        }
        return true;
    }
}
