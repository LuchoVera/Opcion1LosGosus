using System.Data;
using Dapper;

public class DatabaseInitializer
{
    private readonly IDbConnection _dbConnection;

    public DatabaseInitializer(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void InitializeDatabase()
    {
        var createBookTableQuery = @"
            CREATE TABLE IF NOT EXISTS Books (
                Id SERIAL PRIMARY KEY,
                Title VARCHAR(255) NOT NULL,
                Author VARCHAR(255) NOT NULL,
                ISBN VARCHAR(13) UNIQUE NOT NULL,
                Genre VARCHAR(100) NOT NULL,
                PublicationYear INT NOT NULL,
                IsBorrowed BOOLEAN NOT NULL DEFAULT FALSE,
                IsReserved BOOLEAN NOT NULL DEFAULT FALSE
            );
        ";

        var createPatronTableQuery = @"
            CREATE TABLE IF NOT EXISTS Patrons (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(255) NOT NULL,
                MemberShipNumber VARCHAR(50) NOT NULL UNIQUE,
                ContactDetails VARCHAR(255) NOT NULL
            );
        ";

        var createBorrowingRecordTableQuery = @"
            CREATE TABLE IF NOT EXISTS BorrowingRecords (
                Id SERIAL PRIMARY KEY,
                PatronId INT NOT NULL REFERENCES Patrons(Id),
                ISBN VARCHAR(13) NOT NULL REFERENCES Books(ISBN),
                BorrowDate TIMESTAMP NOT NULL,
                DueDate TIMESTAMP NOT NULL,
                ReturnDate TIMESTAMP
            );
        ";

        var createReserveTableQuery = @"
            CREATE TABLE IF NOT EXISTS Reserves (
                Id SERIAL PRIMARY KEY,
                PatronId INT NOT NULL REFERENCES Patrons(Id),
                ISBN VARCHAR(13) NOT NULL REFERENCES Books(ISBN)
            );
        ";

        _dbConnection.Execute(createBookTableQuery);
        _dbConnection.Execute(createPatronTableQuery);
        _dbConnection.Execute(createBorrowingRecordTableQuery);
        _dbConnection.Execute(createReserveTableQuery);
    }
}
