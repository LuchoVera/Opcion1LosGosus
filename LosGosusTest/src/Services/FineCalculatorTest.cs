using LosGosus.Models;
using LosGosus.Services;

namespace LosGosusTest.Services;

public class FineCalculatorTest
{
    // BorrowingRecord borrowing = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddDays(3));
    // BorrowingRecord borrowing2 = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddDays(3));
    [Fact]
    public void Not_OverDue_Borrowing()
    {
        Patron patron = new Patron("juan", "number1", "juan@gmail.com");
        Book book = new Book("book", "jose", "123", "horror", 2024);
        BorrowingRecord borrowing = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddDays(3));

        bool result = FineCalculator.IsOverdue(borrowing);
        Assert.False(result);
    }

    [Fact]
    public void OverDue_Borrowing()
    {
        Patron patron = new Patron("juan", "number1", "juan@gmail.com");
        Book book = new Book("book", "jose", "123", "horror", 2024);
        BorrowingRecord borrowing = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddHours(-3));

        bool result = FineCalculator.IsOverdue(borrowing);
        Assert.True(result);
    }

    [Fact]
    public void Calculate_Fine_For_a_returned_Book()
    {
        Patron patron = new Patron("juan", "number1", "juan@gmail.com");
        Book book = new Book("book", "jose", "123", "horror", 2024);
        BorrowingRecord borrowing = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now);
        borrowing.ReturnDate = DateTime.Now;

        double result = FineCalculator.CalculateFine(borrowing);
        double expected = 0;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Calculate_Fine_For_a_overDue_Book()
    {
        Patron patron = new Patron("juan", "number1", "juan@gmail.com");
        Book book = new Book("book", "jose", "123", "horror", 2024);
        BorrowingRecord borrowing = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddDays(-3));

        double result = FineCalculator.CalculateFine(borrowing);
        double expected = 1.5;

        Assert.Equal(expected, result);
    }

}
