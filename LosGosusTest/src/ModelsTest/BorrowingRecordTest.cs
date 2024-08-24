public class BorrowingRecordTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        Patron patron = new Patron("Juan Pérez", "ptr-2024001", "Calle Bolívar, Sucre");
        Book book = new Book("Historia de Bolivia", "Carlos Mesa", "isbn-9876543210", "History", 2003);
        DateTime borrowDate = new DateTime(2024, 8, 1);
        DateTime dueDate = new DateTime(2024, 8, 15);

        BorrowingRecord record = new BorrowingRecord(patron, book, borrowDate, dueDate);

        Assert.Equal(patron, record.BorrowedBy);
        Assert.Equal(book, record.BorrowedBook);
        Assert.Equal(borrowDate, record.BorrowDate);
        Assert.Equal(dueDate, record.DueDate);
        Assert.Null(record.ReturnDate);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        Patron patron = new Patron("María López", "ptr-2024010", "Av. Hernando Siles, La Paz");
        Book book = new Book("Cultura Aymara", "Jorge Miranda", "isbn-1234567890", "Culture", 1995);
        DateTime borrowDate = new DateTime(2024, 7, 20);
        DateTime dueDate = new DateTime(2024, 8, 3);
        BorrowingRecord record = new BorrowingRecord(patron, book, borrowDate, dueDate);

        string expectedOutput = $"Book: Cultura Aymara, Borrowed on: {borrowDate.ToShortDateString()}\n" +
                                $"Due on: {dueDate.ToShortDateString()}, Returned on: Not returned\n";

        string result = record.ToString();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void ReturnDate_ShouldBeSettable()
    {
        Patron patron = new Patron("Pedro Quispe", "ptr-2024020", "Calle Junín, Cochabamba");
        Book book = new Book("Gastronomía Boliviana", "Laura Alcoba", "isbn-1122334455", "Gastronomy", 2010);
        DateTime borrowDate = new DateTime(2024, 6, 10);
        DateTime dueDate = new DateTime(2024, 6, 24);
        BorrowingRecord record = new BorrowingRecord(patron, book, borrowDate, dueDate);

        record.ReturnDate = new DateTime(2024, 6, 20);

        Assert.Equal(new DateTime(2024, 6, 20), record.ReturnDate);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString_WhenReturned()
    {
        Patron patron = new Patron("Luis Mamani", "ptr-2024030", "Av. América, Cochabamba");
        Book book = new Book("Flora Andina", "Ana Flores", "isbn-6677889900", "Botany", 2005);
        DateTime borrowDate = new DateTime(2024, 5, 5);
        DateTime dueDate = new DateTime(2024, 5, 19);
        BorrowingRecord record = new BorrowingRecord(patron, book, borrowDate, dueDate);
        record.ReturnDate = new DateTime(2024, 5, 15);

        string expectedOutput = $"Book: Flora Andina, Borrowed on: {borrowDate.ToShortDateString()}\n" +
                                $"Due on: {dueDate.ToShortDateString()}, Returned on: {record.ReturnDate.Value.ToShortDateString()}\n";

        string result = record.ToString();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void ReturnDate_ShouldBeNull_WhenNotReturned()
    {
        Patron patron = new Patron("Carlos Gómez", "ptr-2024040", "Calle Murillo, Santa Cruz");
        Book book = new Book("Arte Boliviano", "María Flores", "isbn-9988776655", "Art", 2015);
        DateTime borrowDate = new DateTime(2024, 4, 1);
        DateTime dueDate = new DateTime(2024, 4, 15);
        BorrowingRecord record = new BorrowingRecord(patron, book, borrowDate, dueDate);

        Assert.Null(record.ReturnDate);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString_WhenNotReturned()
    {
        Patron patron = new Patron("Ana Rodríguez", "ptr-2024050", "Av. 6 de Agosto, El Alto");
        Book book = new Book("Música Andina", "Carlos Quispe", "isbn-1122334455", "Music", 2008);
        DateTime borrowDate = new DateTime(2024, 3, 10);
        DateTime dueDate = new DateTime(2024, 3, 24);
        BorrowingRecord record = new BorrowingRecord(patron, book, borrowDate, dueDate);

        string expectedOutput = $"Book: Música Andina, Borrowed on: {borrowDate.ToShortDateString()}\n" +
                                $"Due on: {dueDate.ToShortDateString()}, Returned on: Not returned\n";

        string result = record.ToString();

        Assert.Equal(expectedOutput, result);
    }
}
