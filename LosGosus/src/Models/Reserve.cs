namespace LosGosus.Models;

public class Reserve(Patron reservedBy, Book reservedBook)
{
    public Patron ReservedBy { get; private set; } = reservedBy;
    public Book ReservedBook { get; private set; } = reservedBook;

}
