using System.Text;

public class BookManager : ManagerBase<Book, string>
{
    private Searcher<Book> bookSearcher;

    public BookManager()
    {
        bookSearcher = new Searcher<Book>(new BookSearcher());
    }

    public void ShowBookByTitle(string title) 
    {
        if(bookSearcher != null)
        {
            var books = SearchBooks(book => book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            Paginator.Paginate<Book>(books);
        }
    }

    public void ShowBookByAuthor(string author) 
    {
        if(bookSearcher != null)
        {
            var books = SearchBooks(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            Paginator.Paginate<Book>(books);
        }
    }

    public void ShowBookByISBN(string ISBN) 
    {
        if(bookSearcher != null)
        {
            var book = SearchBook(book => book.ISBN.Equals(ISBN, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(book);
        }
    }

    public void ListBooksByGenre(string genre) 
    {
        if(bookSearcher != null)
        {
            var foundBooks = SearchBooks(book => book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            Paginator.Paginate<Book>(foundBooks);
        }
    }

    public List<Book> SearchBooks(Func<Book, bool> predicate)
    {
        return bookSearcher.SearchMultiple(items, predicate);
    }

    public Book? SearchBook(Func<Book, bool> predicate)
    {
        return bookSearcher.SearchSingle(items, predicate);
    }

    protected override int ReturnIndex (string isbn) 
    {
        Book? book = items.Find(x => x.ISBN == isbn);
        return book != null ? items.IndexOf(book) : -1;
    } 

    public void ListBorrowedBooks() 
    {
        var borrowedBooks = items.FindAll(x => x.IsBorrowed);
        Paginator.Paginate<Book>(borrowedBooks);
    }

    public string GetCurrentBorrowedBooks()
    {
        var borrowedBooks = bookSearcher.SearchMultiple(items, book => book.IsBorrowed);
        var result = new StringBuilder();

        foreach (var book in borrowedBooks)
        {
            result.AppendLine(book.ToString());
        }

        return result.ToString();
    }

    public List<Book> GetBooks()
    {
        return items;
    }
}
