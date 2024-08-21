public class BorrowingRecord
{
    private const double CostPerDay = 0.5;
    public Patron BorrowedBy { get; private set; }
    public Book BorrowedBook { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public BorrowingRecord(Patron borrowedBy, Book borrowedBook, DateTime borrowDate, DateTime dueDate)
    {
        BorrowedBy = borrowedBy;
        BorrowedBook = borrowedBook;
        BorrowDate = borrowDate;
        DueDate = dueDate;
        ReturnDate = null;
    }

    public bool IsOverdue()
    {
        if (ReturnDate.HasValue)
        {
            return false;
        }
        return DateTime.Now > DueDate;
    }

    public double CalculateFine()
    {
        if (ReturnDate.HasValue)
        {
            return 0.0;
        }

        if (!IsOverdue())
        {
            return 0.0;
        }

        int overdueDays = (DateTime.Now - DueDate).Days;
        return overdueDays * CostPerDay;
    }

    public void ReturnBook(DateTime returnDate)
    {
        ReturnDate = returnDate;
        BorrowedBook.Return();
    }

    public override string ToString()
    {
        return $"Book: {BorrowedBook.Title}, Borrowed on: {BorrowDate.ToShortDateString()}\n" +
        $"Due on: {DueDate.ToShortDateString()}, Returned on: {(ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "Not returned")}\n" +
        $"Fine: ${CalculateFine()}\n";
    }
}
