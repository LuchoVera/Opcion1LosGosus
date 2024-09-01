using LosGosus.Controller;

namespace LosGosusTest.Controller;

public class BookControllerTests
{
    private readonly BookController _bookController;

    public BookControllerTests()
    {
        _bookController = new BookController();
    }

    [Fact]
    public void TryAddBook_ValidBook_ReturnsTrue()
    {
        string title = "Test Book";
        string author = "Test Author";
        string isbn = "isbn-1000000000";
        string genre = "Fiction";
        int publicationYear = 2023;

        bool result = _bookController.TryAddBook(title, author, isbn, genre, publicationYear);

        Assert.True(result);
    }

    [Fact]
    public void TryAddBook_InvalidBook_ReturnsFalse()
    {
        string title = "";
        string author = "Test Author";
        string isbn = "isbn-1000000000";
        string genre = "Fiction";
        int publicationYear = 2023;

        bool result = _bookController.TryAddBook(title, author, isbn, genre, publicationYear);

        Assert.False(result);
    }

    [Fact]
    public void TryUpdateBook_ValidBook_ReturnsTrue()
    {
        string title = "Test Book";
        string author = "Test Author";
        string isbn = "isbn-1000000000";
        string genre = "Fiction";
        int publicationYear = 2023;

        _bookController.TryAddBook(title, author, isbn, genre, publicationYear);

        string newTitle = "Updated Book";
        string newAuthor = "Updated Author";
        string newGenre = "Non-Fiction";
        int newPublicationYear = 2024;

        bool result = _bookController.TryUpdateBook(newTitle, newAuthor, isbn, newGenre, newPublicationYear);

        Assert.True(result);
    }

    [Fact]
    public void TryUpdateBook_InvalidBook_ReturnsFalse()
    {
        string title = "Test Book";
        string author = "Test Author";
        string isbn = "isbn-1000000000";
        string genre = "Fiction";
        int publicationYear = 2023;

        _bookController.TryAddBook(title, author, isbn, genre, publicationYear);

        string newTitle = "";
        string newAuthor = "Updated Author";
        string newGenre = "Non-Fiction";
        int newPublicationYear = 2020;

        bool result = _bookController.TryUpdateBook(newTitle, newAuthor, isbn, newGenre, newPublicationYear);

        Assert.False(result);
    }

    [Fact]
    public void DeleteBook_ExistingBook_ReturnsTrue()
    {
        string title = "Test Book";
        string author = "Test Author";
        string isbn = "isbn-1000000000";
        string genre = "Fiction";
        int publicationYear = 2023;

        _bookController.TryAddBook(title, author, isbn, genre, publicationYear);

        bool result = _bookController.DeleteBook(isbn);

        Assert.True(result);
    }

    [Fact]
    public void DeleteBook_NonExistingBook_ReturnsFalse()
    {
        string isbn = "NonExistingISBN";

        bool result = _bookController.DeleteBook(isbn);

        Assert.False(result);
    }
}
