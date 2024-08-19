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

    public void DeleteBook(string isbn)
    {
        bookManager.Delete(isbn);
    }

    public void ListBooks()
    {
        bookManager.List();
    }

    public void ShowBookByTitle(string title)
    {
        bookManager.ShowBookByTitle(title);
    }

    public void ShowBookByAuthor(string author)
    {
        bookManager.ShowBookByAuthor(author);
    }

    public void ShowBookByISBN(string isbn)
    {
        bookManager.ShowBookByISBN(isbn);
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
