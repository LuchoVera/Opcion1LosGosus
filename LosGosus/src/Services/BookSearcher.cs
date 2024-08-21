public class BookSearcher
{
    private List<Book> _books = new List<Book>();

    public BookSearcher(List<Book> books)
    {
        _books = books;
    }

    public List<Book> SearchByTitle(string title)
    {
        return SearchBy(x => x.Title == title);
    }
    public List<Book> SearchByAuthor(string author)
    {
        return SearchBy(x => x.Author == author);
    }
    public Book? SearchByISBN(string isbn)
    {
        var book = _books.Find(x => x.ISBN == isbn);
        return book;
    }

    public List<Book> SearchBy(Predicate<Book> predicate)
    {
        return _books.FindAll(predicate);
    }
}
