using System.Data;
using Dapper;

public class ReserveRepository
{
    private readonly IDbConnection _dbConnection;

    public ReserveRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void AddReserve(Reserve reserve)
    {
        var query = @"
            INSERT INTO Reserves (PatronId, ISBN)
            VALUES (
                (SELECT Id FROM Patrons WHERE Name = @PatronName AND MemberShipNumber = @MemberShipNumber),
                (SELECT Id FROM Books WHERE ISBN = @ISBN)
            );
        ";

        _dbConnection.Execute(query, new
        {
            PatronName = reserve.ReservedBy.Name,
            MemberShipNumber = reserve.ReservedBy.MemberShipNumber,
            ISBN = reserve.ReservedBook.ISBN
        });
    }

    public Reserve? GetReserve(int id)
    {
        var query = @"
            SELECT r.Id, p.Name AS PatronName, p.MemberShipNumber, b.Title, b.Author, b.ISBN, b.Genre, b.PublicationYear, b.IsBorrowed, b.IsReserved
            FROM Reserves r
            INNER JOIN Patrons p ON r.PatronId = p.Id
            INNER JOIN Books b ON r.ISBN = b.ISBN
            WHERE r.Id = @Id;
        ";

        return _dbConnection.Query<Reserve, Patron, Book, Reserve>(
            query,
            (reserve, patron, book) =>
            {
                reserve = new Reserve(patron, book);
                return reserve;
            },
            new { Id = id },
            splitOn: "PatronName,Title"
        ).FirstOrDefault();
    }

    public void UpdateReserve(int id, Reserve reserve)
    {
        var query = @"
            UPDATE Reserves
            SET PatronId = (SELECT Id FROM Patrons WHERE Name = @PatronName AND MemberShipNumber = @MemberShipNumber),
                ISBN = (SELECT Id FROM Books WHERE ISBN = @ISBN)
            WHERE Id = @Id;
        ";

        _dbConnection.Execute(query, new
        {
            Id = id,
            PatronName = reserve.ReservedBy.Name,
            MemberShipNumber = reserve.ReservedBy.MemberShipNumber,
            ISBN = reserve.ReservedBook.ISBN
        });
    }

    public void DeleteReserve(int id)
    {
        var query = @"
            DELETE FROM Reserves
            WHERE Id = @Id;
        ";

        _dbConnection.Execute(query, new { Id = id });
    }

    public IEnumerable<Reserve> GetAllReserves()
    {
        var query = @"
            SELECT r.Id, p.Name AS PatronName, p.MemberShipNumber, b.Title, b.Author, b.ISBN, b.Genre, b.PublicationYear, b.IsBorrowed, b.IsReserved
            FROM Reserves r
            INNER JOIN Patrons p ON r.PatronId = p.Id
            INNER JOIN Books b ON r.ISBN = b.ISBN;
        ";

        return _dbConnection.Query<Reserve, Patron, Book, Reserve>(
            query,
            (reserve, patron, book) =>
            {
                reserve = new Reserve(patron, book);
                return reserve;
            },
            splitOn: "PatronName,Title"
        );
    }
}
