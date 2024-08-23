using System.Text;

public class BookManager : ManagerBase<Book, string>
{
    private BookSearcher? bookSearcher;

    public BookManager()
    {
        bookSearcher = new BookSearcher(items);
    }

    public void ShowBookByTitle(string title) 
    {
        if(bookSearcher != null)
        {
            var books = bookSearcher.SearchByTitle(title);
            Paginator.Paginate<Book>(books);
        }
    }

    public void ShowBookByAuthor(string author) 
    {
        if(bookSearcher != null)
        {
            var books = bookSearcher.SearchByAuthor(author);
            Paginator.Paginate<Book>(books);
        }
    }

    public void ShowBookByISBN(string ISBN) 
    {
        if(bookSearcher != null)
        {
            var book = bookSearcher.SearchByISBN(ISBN);
            Console.WriteLine(book);
        }
    }

    public void ListBooksByGenre(string genre) 
    {
        if(bookSearcher != null)
        {
            var foundBooks = items.FindAll(x => x.Genre == genre);
            Paginator.Paginate<Book>(foundBooks);
        }
    }

    public Book? GetBookByISBN(string isbn) 
    {
        return items.Find(x => x.ISBN == isbn);
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
    public List<Book> SearchBy(Predicate<Book> predicate)
    {
        return items.FindAll(predicate);
    }

    public string GetCurrentBorrowedBooks()
    {
        var borrowedBooks = SearchBy(x => x.IsBorrowed);
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
