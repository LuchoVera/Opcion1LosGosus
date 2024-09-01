using System.Text;

using LosGosus.AbstractClass;
using LosGosus.Managers.Show;
using LosGosus.Managers.Show.ShowBook;
using LosGosus.Models;
using LosGosus.Services;

namespace LosGosus.Managers;

public class BookManager : ManagerBase<Book, string>
{
    private Searcher<Book> bookSearcher;
    private ShowContext<Book> show;

    public BookManager()
    {
        show = new ShowContext<Book>();
        bookSearcher = new Searcher<Book>(new BookSearcher());
    }

    public void ShowBookByTitle(string title) 
    {
        show.SetShow(new ShowBookByTitle());
        show.Execute(items, title);
    }

    public void ShowBookByAuthor(string author) 
    {
        show.SetShow(new ShowBookByAuthor());
        show.Execute(items, author);
    }

    public void ShowBookByISBN(string ISBN) 
    {
        show.SetShow(new ShowBookByISBN());
        show.Execute(items, ISBN);
    }

    public void ListBooksByGenre(string genre) 
    {
        show.SetShow(new ShowBookByGenre());
        show.Execute(items, genre);
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
