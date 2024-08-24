public class ReserveManagerTests
{
    [Fact]
    public void FindReserve_ShouldReturnCorrectReserve()
    {
        var reserveManager = new ReserveManager();
        var patron = new Patron("Carlos", "12345", "carlos@example.com");
        var book = new Book("Cien Años de Soledad", "Gabriel Garcia Marquez", "isbn-1234567890", "Fiction", 1967);
        var reserve = new Reserve(patron, book);
        reserveManager.Add(reserve);

        var result = reserveManager.FindReserve(book);

        Assert.NotNull(result);
        Assert.Equal(reserve, result);
    }

    [Fact]
    public void FindReserve_ShouldReturnNull_WhenReserveNotFound()
    {
        var reserveManager = new ReserveManager();
        var book = new Book("Cien Años de Soledad", "Gabriel Garcia Marquez", "isbn-1234567890", "Fiction", 1967);

        var result = reserveManager.FindReserve(book);

        Assert.Null(result);
    }
}
