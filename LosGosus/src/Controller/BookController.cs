using LosGosus.Managers;
using LosGosus.Models;
using LosGosus.Validators.Concretes;

namespace LosGosus.Controller;

public class BookController
{
    private BookManager bookManager = new BookManager();
    private BookValidator bookValidator = new BookValidator();

    public bool TryAddBook(string title, string author, string isbn, string genre, int publicationYear)
    {
        Book book = new Book(title, author, isbn, genre, publicationYear);
        if (bookValidator.Validate(book))
        {
            bookManager.Add(book);
            return true;
        }
        return false;
    }

    public bool TryUpdateBook(string title, string author, string isbn, string genre, int publicationYear)
    {
        Book book = new Book(title, author, isbn, genre, publicationYear);
        if (bookValidator.Validate(book))
        {
            bookManager.Update(book, isbn);
            return true;
        }
        return false;
    }

    public bool DeleteBook(string isbn)
    {
        if (bookManager.SearchBook(book => book.ISBN
            .Equals(isbn, StringComparison.OrdinalIgnoreCase)) == null)
        {
            return false;
        }
        bookManager.Delete(isbn);
        return true;
    }

    public void ListBooks()
    {
        bookManager.List();
    }

    public void ListBooksByGenre(string genre)
    {
        bookManager.ListBooksByGenre(genre);
    }

    public BookManager GetBookManager()
    {
        return bookManager;
    }
}
