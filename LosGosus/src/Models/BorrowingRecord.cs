public class BorrowingRecord
{
    public Patron BorrowedBy { get; private set; }
    public Book BorrowedBook { get; set; }
    public DateTime BorrowDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; set; }

    public BorrowingRecord(Patron borrowedBy, Book borrowedBook, DateTime borrowDate, DateTime dueDate)
    {
        BorrowedBy = borrowedBy;
        BorrowedBook = borrowedBook;
        BorrowDate = borrowDate;
        DueDate = dueDate;
        ReturnDate = null;
    }

    public override string ToString()
    {
        return $"Book: {BorrowedBook.Title}, Borrowed on: {BorrowDate.ToShortDateString()}\n" +
        $"Due on: {DueDate.ToShortDateString()}, Returned on: {(ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "Not returned")}\n";
    }
}
