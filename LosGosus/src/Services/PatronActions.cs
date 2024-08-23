public class PatronActions
{
    private BorrowingManager borrowingManager;
    private PatronManager patronManager;
    private BookManager bookManager;
    private ReserveManager ReserveManager;
    private const int maxBorrowDays = 14;

    public PatronActions(BorrowingManager borrowingManager, PatronManager patronManager, BookManager bookManager, ReserveManager reserveManager)
    {
        this.borrowingManager = borrowingManager;
        this.patronManager = patronManager;
        this.bookManager = bookManager;
        ReserveManager = reserveManager;
    }

    public void BorrowBook(string membershipNumber, string bookISBN)
    {
        Patron? patron = patronManager.GetPatronByMembershipNumber(membershipNumber);
        Book? book = bookManager.GetBookByISBN(bookISBN);

        if (book == null || patron == null)
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book or patron MembershipNumber."));
            return;
        }

        if (book.IsBorrowed)
        {
            ErrorHandler.HandleError(new InvalidBookException("The book is already borrowed."));
            return;
        }

        BorrowingRecord record = new BorrowingRecord(patron, book, DateTime.Now, DateTime.Now.AddDays(maxBorrowDays));
        borrowingManager.Add(record);
        patron.BorrowingRecords.Add(record);
        book.IsBorrowed = !book.IsReserved || book.IsBorrowed;
        Console.WriteLine("Book borrowed successfully.");
    }

    public void ReturnBook(string membershipNumber, string bookISBN)
    {
        Patron? patron = patronManager.GetPatronByMembershipNumber(membershipNumber);
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

        record.ReturnDate = DateTime.Now;
        record.BorrowedBook.IsBorrowed = false;
        ReserveVerify(book);
        book.IsBorrowed = false;
        Console.WriteLine("Book returned successfully. Fine: $" + FineCalculator.CalculateFine(record));
    }

    private void ReserveVerify(Book book)
    {
        if (book.IsReserved)
        {
            Reserve? reserve = ReserveManager.FindReserve(book);
            if (reserve != null)
            {
                book.IsReserved = false;
                BorrowBook(reserve.ReservedBy.MemberShipNumber, book.ISBN);
            }
        }
    }

    public void ReserveBook(string membershipNumber, string bookISBN)
    {
        Patron? patron = patronManager.GetPatronByMembershipNumber(membershipNumber);
        Book? book = bookManager.GetBookByISBN(bookISBN);

        if (book == null || patron == null)
        {
            Console.WriteLine("Invalid book or patron ID.");
            return;
        }

        if (book.IsReserved)
        {
            Console.WriteLine("The book is already reserved.");
            return;
        }

        Reserve reserve = new(patron, book);
        ReserveManager.Add(reserve);
        patron.ReservedRecords.Add(reserve);
        book.IsReserved = book.IsBorrowed || book.IsReserved;
    }

    public void PrintBorrowingHistory(string membershipNumber)
    {
        Patron? patron = patronManager.GetPatronByMembershipNumber(membershipNumber);

        if (patron == null)
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book or patron ID."));
            return;
        }

        List<BorrowingRecord> history = patron.BorrowingRecords;
        foreach (var record in history)
        {
            Console.WriteLine($"Book: {record.BorrowedBook.Title}, Borrowed on: {record.BorrowDate.ToShortDateString()}, Due on: {record.DueDate.ToShortDateString()}, Returned on: {(record.ReturnDate.HasValue ? record.ReturnDate.Value.ToShortDateString() : "Not returned")}, Fine: ${FineCalculator.CalculateFine(record)}");
        }
    }

}
