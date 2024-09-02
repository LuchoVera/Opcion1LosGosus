using LMS.Business.Services;
using LMS.DataAccess.Entities;
using LMS.DataAccess.Interfaces;

namespace LMS.Business.Managers.Show.ShowBook;

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
