public class BookTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        string title = "Book";
        string author = "Author";
        string isbn = "isbn-1000000000";
        string genre = "genre";
        int publicationYear = 1945;

        Book book = new Book(title, author, isbn, genre, publicationYear);

        Assert.Equal(title, book.Title);
        Assert.Equal(author, book.Author);
        Assert.Equal(isbn, book.ISBN);
        Assert.Equal(genre, book.Genre);
        Assert.Equal(publicationYear, book.PublicationYear);
        Assert.False(book.IsBorrowed);
        Assert.False(book.IsReserved);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        Book book = new Book("Title", "Author", "isbn-1000000000", "Genre", 1925);

        string expectedOutput = "Title: Title, Author: Author\n" +
                                "Genre: Genre, Publication Year: 1925\n" +
                                "Is Borrowed: False, Is Reserved: False\n" +
                                "ISBN: isbn-1000000000\n";

        string result = book.ToString();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void IsBorrowed_ShouldBeSettable()
    {
        Book book = new Book("Title", "Author", "isbn-1000000000", "Genre", 1925);

        book.IsBorrowed = true;

        Assert.True(book.IsBorrowed);
    }

    [Fact]
    public void IsReserved_ShouldBeSettable()
    {
        Book book = new Book("Title", "Author", "isbn-1000000000", "Genre", 1925);

        book.IsReserved = true;

        Assert.True(book.IsReserved);
    }

}
