public class ReserveTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        Patron patron = new Patron("Carlos Rojas", "ptr-2024040", "Calle Camacho, La Paz");
        Book book = new Book("Mitos y Leyendas de Bolivia", "Ernesto Cavour", "isbn-9988776655", "Folklore", 1987);

        Reserve reserve = new Reserve(patron, book);

        Assert.Equal(patron, reserve.ReservedBy);
        Assert.Equal(book, reserve.ReservedBook);
    }

    [Fact]
    public void ReservedBy_ShouldReturnCorrectPatron()
    {
        Patron patron = new Patron("Ana Suárez", "ptr-2024050", "Calle 25 de Mayo, Sucre");
        Book book = new Book("Historia de la Quinua", "Luis Delgado", "isbn-5544332211", "Agriculture", 2015);
        Reserve reserve = new Reserve(patron, book);

        Patron result = reserve.ReservedBy;

        Assert.Equal(patron, result);
    }

    [Fact]
    public void ReservedBook_ShouldReturnCorrectBook()
    {
        Patron patron = new Patron("José Fernández", "ptr-2024060", "Calle Tumusla, Oruro");
        Book book = new Book("Arquitectura de Bolivia", "Pedro Ruiz", "isbn-4433221100", "Architecture", 2001);
        Reserve reserve = new Reserve(patron, book);

        Book result = reserve.ReservedBook;

        Assert.Equal(book, result);
    }
}
