using LMS.Business.Interfaces;
using LMS.DataAccess.Entities;

namespace LMS.Business.Services;

public class BookSearcher : ISearch<Book>
{
    public List<Book> SearchMultiple(List<Book> items, Func<Book, bool> predicate)
    {
        return items.Where(predicate).ToList();
    }

    public Book? SearchSingle(List<Book> items, Func<Book, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }
}
