using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class BorrowingManagerTests
{
    [Fact]
    public void FindBorrowingRecord_ShouldReturnCorrectRecord()
    {
        // Arrange
        var borrowingManager = new BorrowingManager();
        var patron = new Patron("Carlos", "12345", "carlos@example.com");
        var book = new Book("Cien Años de Soledad", "Gabriel Garcia Marquez", "isbn-1234567890", "Fiction", 1967);
        var record = new BorrowingRecord(patron, book, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));

        borrowingManager.GetBorrowingRecords().Add(record);

        // Act
        var result = borrowingManager.FindBorrowingRecord(patron, book);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(record, result);
    }

    [Fact]
    public void FindBorrowingRecord_ShouldReturnNull_WhenRecordNotFound()
    {
        // Arrange
        var borrowingManager = new BorrowingManager();
        var patron = new Patron("Carlos", "12345", "carlos@example.com");
        var book = new Book("Cien Años de Soledad", "Gabriel Garcia Marquez", "isbn-1234567890", "Fiction", 1967);

        // Act
        var result = borrowingManager.FindBorrowingRecord(patron, book);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetOverdueBooks_ShouldReturnEmptyString_WhenNoOverdueBooks()
    {
        // Arrange
        var borrowingManager = new BorrowingManager();
        var patron = new Patron("Luis", "67890", "luis@example.com");
        var book = new Book("El Túnel", "Ernesto Sabato", "isbn-1122334455", "Psychological", 1948);
        var record = new BorrowingRecord(patron, book, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));

        // Manually setting the private borrowingRecords list
        borrowingManager.GetBorrowingRecords().Add(record);

        // Act
        var overdueBooks = borrowingManager.GetOverdueBooks();

        // Assert
        Assert.Empty(overdueBooks);
    }

    [Fact]
    public void GetBorrowingHistory_ShouldReturnEmptyString_WhenNoHistory()
    {
        // Arrange
        var borrowingManager = new BorrowingManager();
        var patron = new Patron("Luis", "11223", "luis@example.com");

        // Act
        var history = borrowingManager.GetBorrowingHistory(patron.MemberShipNumber);

        // Assert
        Assert.Empty(history);
    }

    [Fact]
    public void GetBorrowingRecords_ShouldReturnAllRecords()
    {
        // Arrange
        var borrowingManager = new BorrowingManager();
        var patron = new Patron("Andrea", "44556", "andrea@example.com");
        var book = new Book("El Señor Presidente", "Miguel Ángel Asturias", "isbn-6677889900", "Political", 1946);
        var record = new BorrowingRecord(patron, book, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(8));

        borrowingManager.GetBorrowingRecords().Add(record);

        // Act
        var records = borrowingManager.GetBorrowingRecords();

        // Assert
        Assert.Single(records);
        Assert.Equal(record, records.First());
    }

    [Fact]
    public void GetBorrowingRecords_ShouldReturnEmptyList_WhenNoRecordsExist()
    {
        // Arrange
        var borrowingManager = new BorrowingManager();

        // Act
        var records = borrowingManager.GetBorrowingRecords();

        // Assert
        Assert.Empty(records);
    }
}
