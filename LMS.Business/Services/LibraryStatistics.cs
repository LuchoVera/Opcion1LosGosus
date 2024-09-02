using LMS.Business.Managers;

namespace LMS.Business.Services;

public class LibraryStatistics
{
    private BookManager bookManager;
    private BorrowingManager borrowingManager;
    private PatronManager patronManager;
    private const int DefaultTopN = 5;

    public LibraryStatistics(BookManager bookManager, BorrowingManager borrowingManager, PatronManager patronManager)
    {
        this.bookManager = bookManager;
        this.borrowingManager = borrowingManager;
        this.patronManager = patronManager;
    }

    public List<(string Title, int BorrowCount)> GetMostBorrowedBooks(int topN = DefaultTopN)
    {
        var borrowCounts = borrowingManager
            .GetBorrowingRecords()
            .GroupBy(record => record.BorrowedBook.Title)
            .Select(group => (Title: group.Key, BorrowCount: group.Count()))
            .OrderByDescending(b => b.BorrowCount)
            .Take(topN)
            .ToList();

        return borrowCounts;
    }

    public List<(string PatronName, int BorrowCount)> GetMostActivePatrons(int topN = DefaultTopN)
    {
        var activePatrons = borrowingManager
            .GetBorrowingRecords()
            .GroupBy(record => record.BorrowedBy.Name)
            .Select(group => (PatronName: group.Key, BorrowCount: group.Count()))
            .OrderByDescending(p => p.BorrowCount)
            .Take(topN)
            .ToList();

        return activePatrons;
    }

    public List<(string Genre, int Count)> GetBooksByGenre()
    {
        var booksByGenre = bookManager
            .GetBooks()
            .GroupBy(book => book.Genre)
            .Select(group => (Genre: group.Key, Count: group.Count()))
            .OrderByDescending(g => g.Count)
            .ToList();

        return booksByGenre;
    }
}
