public class BookSearcherTests
{
    private readonly BookSearcher _searcher;

    public BookSearcherTests()
    {
        _searcher = new BookSearcher();
    }

    [Fact]
    public void SearchMultiple_ShouldReturnMatchingBooks()
    {
        var books = new List<Book>
        {
            new Book("Title1", "Author1", "isbn-1000000000", "Fiction", 2021),
            new Book("Title2", "Author2", "isbn-1000000001", "Non-Fiction", 2022),
            new Book("Title3", "Author1", "isbn-1000000002", "Fiction", 2023)
        };
        Func<Book, bool> predicate = b => b.Author == "Author1";

        var result = _searcher.SearchMultiple(books, predicate);

        Assert.Equal(2, result.Count);
        Assert.All(result, b => Assert.Equal("Author1", b.Author));
    }

    [Fact]
    public void SearchMultiple_ShouldReturnEmptyList_WhenNoBooksMatch()
    {
        var books = new List<Book>
        {
            new Book("Title1", "Author1", "isbn-1000000000", "Fiction", 2021),
            new Book("Title2", "Author2", "isbn-1000000001", "Non-Fiction", 2022)
        };
        Func<Book, bool> predicate = b => b.Author == "Non-Existing Author";

        var result = _searcher.SearchMultiple(books, predicate);

        Assert.Empty(result);
    }

    [Fact]
    public void SearchSingle_ShouldReturnMatchingBook()
    {
        var books = new List<Book>
        {
            new Book("Title1", "Author1", "isbn-1000000000", "Fiction", 2021),
            new Book("Title2", "Author2", "isbn-1000000001", "Non-Fiction", 2022),
            new Book("Title3", "Author1", "isbn-1000000002", "Fiction", 2023)
        };
        Func<Book, bool> predicate = b => b.ISBN == "isbn-1000000001";

        var result = _searcher.SearchSingle(books, predicate);

        Assert.NotNull(result);
        Assert.Equal("isbn-1000000001", result.ISBN);
    }

    [Fact]
    public void SearchSingle_ShouldReturnNull_WhenNoBookMatches()
    {
        var books = new List<Book>
        {
            new Book("Title1", "Author1", "isbn-1000000000", "Fiction", 2021),
            new Book("Title2", "Author2", "isbn-1000000001", "Non-Fiction", 2022)
        };
        Func<Book, bool> predicate = b => b.ISBN == "isbn-1000000003";

        var result = _searcher.SearchSingle(books, predicate);

        Assert.Null(result);
    }
}
