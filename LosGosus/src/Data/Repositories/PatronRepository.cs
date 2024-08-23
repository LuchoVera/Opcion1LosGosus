using System.Data;
using Dapper;

public class PatronRepository
{
    private readonly IDbConnection _dbConnection;

    public PatronRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void AddPatron(Patron patron)
    {
        var query = @"
            INSERT INTO Patrons (Name, MemberShipNumber, ContactDetails)
            VALUES (@Name, @MemberShipNumber, @ContactDetails);
        ";

        _dbConnection.Execute(query, new
        {
            patron.Name,
            patron.MemberShipNumber,
            patron.ContactDetails
        });
    }

    public Patron? GetPatron(int id)
    {
        var query = @"
            SELECT Id, Name, MemberShipNumber, ContactDetails
            FROM Patrons
            WHERE Id = @Id;
        ";

        return _dbConnection.QuerySingleOrDefault<Patron>(query, new { Id = id });
    }

    public void UpdatePatron(int id, Patron patron)
    {
        var query = @"
            UPDATE Patrons
            SET Name = @Name, MemberShipNumber = @MemberShipNumber, ContactDetails = @ContactDetails
            WHERE Id = @Id;
        ";

        _dbConnection.Execute(query, new
        {
            Id = id,
            patron.Name,
            patron.MemberShipNumber,
            patron.ContactDetails
        });
    }

    public void DeletePatron(int id)
    {
        var query = @"
            DELETE FROM Patrons
            WHERE Id = @Id;
        ";

        _dbConnection.Execute(query, new { Id = id });
    }

    public IEnumerable<Patron> GetAllPatrons()
    {
        var query = @"
            SELECT Id, Name, MemberShipNumber, ContactDetails
            FROM Patrons;
        ";

        return _dbConnection.Query<Patron>(query);
    }
}
