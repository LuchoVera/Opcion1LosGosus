public class PatronActions
{
    private BorrowingManager borrowingManager;
    private PatronManager patronManager;
    private BookManager bookManager;
    private const int maxBorrowDays = 14;

    public PatronActions(BorrowingManager borrowingManager, PatronManager patronManager, BookManager bookManager)
    {
        this.borrowingManager = borrowingManager;
        this.patronManager = patronManager;
        this.bookManager = bookManager;
    }

    public void BorrowBook(string patronId, string bookISBN)
    {
        Patron? patron = patronManager.GetPatronById(patronId);
        Book? book = bookManager.GetBookByISBN(bookISBN);

        if (book == null || patron == null)
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book or patron ID."));
            return;
        }

        if (book.IsBorrowed)
        {
            ErrorHandler.HandleError(new InvalidBookException("The book is already borrowed."));
            return;
        }

        BorrowingRecord record = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddDays(maxBorrowDays));
        borrowingManager.AddBorrowingRecord(record);
        patron.AddBorrowingRecord(record);
        book.Borrow();
        Console.WriteLine("Book borrowed successfully.");
    }

    public void ReturnBook(string patronId, string bookISBN)
    {
        Patron? patron = patronManager.GetPatronById(patronId);
        Book? book = bookManager.GetBookByISBN(bookISBN);

        if (book == null || patron == null)
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book or patron ID."));
            return;
        }

        BorrowingRecord? record = borrowingManager.FindBorrowingRecord(patron, book);
        if (record == null)
        {
            Console.WriteLine("No borrowing record found for this book and patron.");
            return;
        }

        record.ReturnBook(DateTime.Now);
        book.Return();
        Console.WriteLine("Book returned successfully. Fine: $" + record.CalculateFine());
    }

    public void PrintBorrowingHistory(string patronId)
    {
        Patron? patron = patronManager.GetPatronById(patronId);

        if (patron == null)
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book or patron ID."));
            return;
        }

        List<BorrowingRecord> history = patron.GetBorrowingHistory();
        foreach (var record in history)
        {
            Console.WriteLine($"Book: {record.BorrowedBook.Title}, Borrowed on: {record.BorrowDate.ToShortDateString()}, Due on: {record.DueDate.ToShortDateString()}, Returned on: {(record.ReturnDate.HasValue ? record.ReturnDate.Value.ToShortDateString() : "Not returned")}, Fine: ${record.CalculateFine()}");
        }
    }
    
}
