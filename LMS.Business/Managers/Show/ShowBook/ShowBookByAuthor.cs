using LMS.Business.Services;
using LMS.DataAccess.Entities;
using LMS.DataAccess.Interfaces;

namespace LMS.Business.Managers.Show.ShowBook;

public class ShowBookByAuthor : IShow<Book>
{
    private Searcher<Book> bookSearcher;

    public ShowBookByAuthor()
    {
        bookSearcher = new Searcher<Book>(new BookSearcher());
    }
    public void ShowResult(List<Book> items, string criteria)
    {
        if(bookSearcher != null)
        {
            var books = bookSearcher.SearchMultiple(items, (book => book.Author.Contains(criteria, StringComparison.OrdinalIgnoreCase)));
            Paginator.Paginate<Book>(books);
        }
    }
}
