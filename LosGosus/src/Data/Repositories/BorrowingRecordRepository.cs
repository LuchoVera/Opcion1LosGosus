using System.Data;
using Dapper;

public class BorrowingRecordRepository
{
    private readonly IDbConnection _dbConnection;

    public BorrowingRecordRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void AddBorrowingRecord(BorrowingRecord record)
    {
        var query = @"
            INSERT INTO BorrowingRecords (PatronId, ISBN, BorrowDate, DueDate, ReturnDate)
            VALUES (
                (SELECT Id FROM Patrons WHERE Name = @PatronName AND MemberShipNumber = @MemberShipNumber),
                (SELECT ISBN FROM Books WHERE ISBN = @ISBN),
                @BorrowDate, @DueDate, @ReturnDate
            );
        ";

        _dbConnection.Execute(query, new
        {
            PatronName = record.BorrowedBy.Name,
            MemberShipNumber = record.BorrowedBy.MemberShipNumber,
            ISBN = record.BorrowedBook.ISBN,
            record.BorrowDate,
            record.DueDate,
            ReturnDate = record.ReturnDate.HasValue ? record.ReturnDate.Value : (DateTime?)null
        });
    }

    public BorrowingRecord? GetBorrowingRecord(int id)
    {
        var query = @"
            SELECT br.Id, p.Name AS PatronName, p.MemberShipNumber, b.Title, b.Author, b.ISBN, b.Genre, b.PublicationYear, b.IsBorrowed, b.IsReserved, br.BorrowDate, br.DueDate, br.ReturnDate
            FROM BorrowingRecords br
            INNER JOIN Patrons p ON br.PatronId = p.Id
            INNER JOIN Books b ON br.ISBN = b.ISBN
            WHERE br.Id = @Id;
        ";

        return _dbConnection.Query<BorrowingRecord, Patron, Book, BorrowingRecord>(
            query,
            (record, patron, book) =>
            {
                record = new BorrowingRecord(patron, book, record.BorrowDate, record.DueDate);
                if (record.ReturnDate.HasValue)
                {
                    record.ReturnBook(record.ReturnDate.Value);
                }
                return record;
            },
            new { Id = id },
            splitOn: "PatronName,Title"
        ).FirstOrDefault();
    }

    public void UpdateBorrowingRecord(int id, BorrowingRecord record)
    {
        var query = @"
            UPDATE BorrowingRecords
            SET PatronId = (SELECT Id FROM Patrons WHERE Name = @PatronName AND MemberShipNumber = @MemberShipNumber),
                ISBN = (SELECT ISBN FROM Books WHERE ISBN = @ISBN),
                BorrowDate = @BorrowDate,
                DueDate = @DueDate,
                ReturnDate = @ReturnDate
            WHERE Id = @Id;
        ";

        _dbConnection.Execute(query, new
        {
            Id = id,
            PatronName = record.BorrowedBy.Name,
            MemberShipNumber = record.BorrowedBy.MemberShipNumber,
            ISBN = record.BorrowedBook.ISBN,
            record.BorrowDate,
            record.DueDate,
            ReturnDate = record.ReturnDate.HasValue ? record.ReturnDate.Value : (DateTime?)null
        });
    }

    public void DeleteBorrowingRecord(int id)
    {
        var query = @"
            DELETE FROM BorrowingRecords
            WHERE Id = @Id;
        ";

        _dbConnection.Execute(query, new { Id = id });
    }

    public IEnumerable<BorrowingRecord> GetAllBorrowingRecords()
    {
        var query = @"
            SELECT br.Id, p.Name AS PatronName, p.MemberShipNumber, b.Title, b.Author, b.ISBN, b.Genre, b.PublicationYear, b.IsBorrowed, b.IsReserved, br.BorrowDate, br.DueDate, br.ReturnDate
            FROM BorrowingRecords br
            INNER JOIN Patrons p ON br.PatronId = p.Id
            INNER JOIN Books b ON br.ISBN = b.ISBN;
        ";

        return _dbConnection.Query<BorrowingRecord, Patron, Book, BorrowingRecord>(
            query,
            (record, patron, book) =>
            {
                record = new BorrowingRecord(patron, book, record.BorrowDate, record.DueDate);
                if (record.ReturnDate.HasValue)
                {
                    record.ReturnBook(record.ReturnDate.Value);
                }
                return record;
            },
            splitOn: "PatronName,Title"
        );
    }
}
