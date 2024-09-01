using LosGosus.Models;
using LosGosus.Services;

namespace LosGosus.Managers.Show.ShowBook;

public class ShowBookByISBN : IShow<Book>
{
    private Searcher<Book> bookSearcher;

    public ShowBookByISBN()
    {
        bookSearcher = new Searcher<Book>(new BookSearcher());
    }
    public void ShowResult(List<Book> items, string criteria)
    {
        if(bookSearcher != null)
        {
            var book = bookSearcher.SearchSingle(items, (book => book.ISBN.Contains(criteria, StringComparison.OrdinalIgnoreCase)));
            Console.WriteLine(book);
        }
    }
}
