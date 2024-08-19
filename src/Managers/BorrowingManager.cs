public class BorrowingManager {
    private List<BorrowingRecord> borrowingRecords;

    public BorrowingManager() {
        borrowingRecords = new List<BorrowingRecord>();
    }

    public void AddBorrowingRecord(BorrowingRecord record) {
        borrowingRecords.Add(record);
    }

    public BorrowingRecord? FindBorrowingRecord(Patron patron, Book book) {
        var record = borrowingRecords.Find(r => r.BorrowedBook == book && r.BorrowedBy == patron && !r.ReturnDate.HasValue);
        return record;
    }

    public string GetOverdueBooks()
    {
        var overdueBooks = borrowingRecords
            .Where(record => record.IsOverdue())
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, overdueBooks);
    }

    public string GetBorrowingHistory(string patronId)
    {
        var history = borrowingRecords
            .Where(record => record.BorrowedBy.PatronId == patronId)
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, history);
    }

}