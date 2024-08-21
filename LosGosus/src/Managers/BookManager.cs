using System.Text;

public class BookManager : IManager<Book, string> 
{
    private List<Book> books = new List<Book>();
    private BookSearcher? bookSearcher;

    public BookManager()
    {
        bookSearcher = new BookSearcher(books);
    }

    public void Add(Book book) 
    {
        books.Add(book);
    }

    public void Update(Book book, string isbn) 
    {
        books[ReturnIndex(isbn)] = book;
    }

    public void Delete(string isbn) 
    {
        books.RemoveAt(ReturnIndex(isbn));
    }

    public void List() 
    {
        foreach (Book book in books) 
        {
            Console.WriteLine(book);
        }
    }

    public void ShowBookByTitle(string title) 
    {
        if(bookSearcher != null)
        {
            var books = bookSearcher.SearchByTitle(title);
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
    }

    public void ShowBookByAuthor(string author) 
    {
        if(bookSearcher != null)
        {
            var books = bookSearcher.SearchByAuthor(author);
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
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
            var foundBooks = books.FindAll(x => x.Genre == genre);
            foreach (var book in foundBooks)
            {
                Console.WriteLine(book);
            }
        }
    }

    public Book? GetBookByISBN(string isbn) 
    {
        return books.Find(x => x.ISBN == isbn);
    }
    private int ReturnIndex (string isbn) 
    {
        Book? book = books.Find(x => x.ISBN == isbn);
        return book != null ? books.IndexOf(book) : -1;
    } 

    public void ListBorrowedBooks() 
    {
        var borrowedBooks = books.FindAll(x => x.IsBorrowed);
        foreach (var book in borrowedBooks)
        {
            Console.WriteLine(book);
        }
    }
    public List<Book> SearchBy(Predicate<Book> predicate)
    {
        return books.FindAll(predicate);
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
        return books;
    }
    

}
