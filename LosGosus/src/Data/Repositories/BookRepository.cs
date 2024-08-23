using System.Data;
using Dapper;

public class BookRepository
{
    private readonly IDbConnection _dbConnection;

    public BookRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void AddBook(Book book)
    {
        var query = @"
            INSERT INTO Books (Title, Author, ISBN, Genre, PublicationYear, IsBorrowed, IsReserved)
            VALUES (@Title, @Author, @ISBN, @Genre, @PublicationYear, @IsBorrowed, @IsReserved);
        ";

        _dbConnection.Execute(query, new
        {
            book.Title,
            book.Author,
            book.ISBN,
            book.Genre,
            book.PublicationYear,
            book.IsBorrowed,
            book.IsReserved
        });
    }

    public Book? GetBook(int id)
    {
        var query = @"
            SELECT Id, Title, Author, ISBN, Genre, PublicationYear, IsBorrowed, IsReserved
            FROM Books
            WHERE Id = @Id;
        ";

        return _dbConnection.QuerySingleOrDefault<Book>(query, new { Id = id });
    }

    public void UpdateBook(Book book)
    {
        var query = @"
            UPDATE Books
            SET Title = @Title, Author = @Author, ISBN = @ISBN, Genre = @Genre,
                PublicationYear = @PublicationYear, IsBorrowed = @IsBorrowed, IsReserved = @IsReserved
            WHERE ISBN = @ISBN;
        ";

        _dbConnection.Execute(query, new
        {
            book.Title,
            book.Author,
            book.ISBN,
            book.Genre,
            book.PublicationYear,
            book.IsBorrowed,
            book.IsReserved
        });
    }

    public void DeleteBook(string ISBN)
    {
        var query = @"
            DELETE FROM Books
            WHERE ISBN = @ISBN;
        ";

        _dbConnection.Execute(query, new { ISBN = ISBN });
    }

    public IEnumerable<Book> GetAllBooks()
    {
        var query = @"
            SELECT Id, Title, Author, ISBN, Genre, PublicationYear, IsBorrowed, IsReserved
            FROM Books;
        ";

        return _dbConnection.Query<Book>(query);
    }
}
