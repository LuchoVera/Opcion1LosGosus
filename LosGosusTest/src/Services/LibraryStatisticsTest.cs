using LosGosus.Managers;
using LosGosus.Models;
using LosGosus.Services;

namespace LosGosusTest.Services;

public class LibraryStatisticsTests
{
    private readonly LibraryStatistics _libraryStatistics;
    private readonly BookManager _bookManager;
    private readonly BorrowingManager _borrowingManager;
    private readonly PatronManager _patronManager;

    public LibraryStatisticsTests()
    {
        _bookManager = new BookManager();
        _borrowingManager = new BorrowingManager();
        _patronManager = new PatronManager();
        _libraryStatistics = new LibraryStatistics(_bookManager, _borrowingManager, _patronManager);
    }

    [Fact]
    public void GetMostBorrowedBooks_ReturnsCorrectTopN()
    {
        var book1 = new Book("Book 1", "Author 1", "isbn-1", "Fiction", 2023);
        var book2 = new Book("Book 2", "Author 2", "isbn-2", "Fiction", 2022);
        var book3 = new Book("Book 3", "Author 3", "isbn-3", "Non-Fiction", 2021);
        _bookManager.Add(book1);
        _bookManager.Add(book2);
        _bookManager.Add(book3);

        var patron = new Patron("Patron 1", "membership-1", "Contact 1");
        _patronManager.Add(patron);

        _borrowingManager.Add(new BorrowingRecord(patron, book1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10)));
        _borrowingManager.Add(new BorrowingRecord(patron, book1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(15)));
        _borrowingManager.Add(new BorrowingRecord(patron, book2, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(20)));
        
        var result = _libraryStatistics.GetMostBorrowedBooks(2);

        Assert.Equal(2, result.Count);
        Assert.Equal("Book 1", result[0].Title);
        Assert.Equal(2, result[0].BorrowCount);
        Assert.Equal("Book 2", result[1].Title);
        Assert.Equal(1, result[1].BorrowCount);
    }

    [Fact]
    public void GetMostActivePatrons_ReturnsCorrectTopN()
    {
        var book1 = new Book("Book 1", "Author 1", "isbn-1", "Fiction", 2023);
        _bookManager.Add(book1);

        var patron1 = new Patron("Patron 1", "membership-1", "Contact 1");
        var patron2 = new Patron("Patron 2", "membership-2", "Contact 2");
        _patronManager.Add(patron1);
        _patronManager.Add(patron2);

        _borrowingManager.Add(new BorrowingRecord(patron1, book1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10)));
        _borrowingManager.Add(new BorrowingRecord(patron1, book1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(15)));
        _borrowingManager.Add(new BorrowingRecord(patron2, book1, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(20)));

        var result = _libraryStatistics.GetMostActivePatrons(1);

        Assert.Single(result);
        Assert.Equal("Patron 1", result[0].PatronName);
        Assert.Equal(2, result[0].BorrowCount);
    }

    [Fact]
    public void GetBooksByGenre_ReturnsBooksGroupedByGenre()
    {
        var book1 = new Book("Book 1", "Author 1", "isbn-1", "Fiction", 2023);
        var book2 = new Book("Book 2", "Author 2", "isbn-2", "Non-Fiction", 2022);
        var book3 = new Book("Book 3", "Author 3", "isbn-3", "Fiction", 2021);
        _bookManager.Add(book1);
        _bookManager.Add(book2);
        _bookManager.Add(book3);

        var result = _libraryStatistics.GetBooksByGenre();

        Assert.Equal(2, result.Count);
        Assert.Equal("Fiction", result[0].Genre);
        Assert.Equal(2, result[0].Count);
        Assert.Equal("Non-Fiction", result[1].Genre);
        Assert.Equal(1, result[1].Count);
    }
}
