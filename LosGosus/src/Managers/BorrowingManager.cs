
public class BorrowingManager : ManagerBase<BorrowingRecord, string>
{
    private List<BorrowingRecord> borrowingRecords;

    public BorrowingManager()
    {
        borrowingRecords = new List<BorrowingRecord>();
    }

    public BorrowingRecord? FindBorrowingRecord(Patron patron, Book book)
    {
        var record = items.Find(r => r.BorrowedBook == book && r.BorrowedBy == patron && !r.ReturnDate.HasValue);
        return record;
    }

    public string GetOverdueBooks()
    {
        var overdueBooks = borrowingRecords
            .Where(FineCalculator.IsOverdue)
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, overdueBooks);
    }

    public string GetBorrowingHistory(string membershipNumber)
    {
        var history = borrowingRecords
            .Where(record => record.BorrowedBy.MemberShipNumber == membershipNumber)
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, history);
    }

    public List<BorrowingRecord> GetBorrowingRecords()
    {
        return items;
    }

    protected override int ReturnIndex(string code)
    {
        var borrowingRecord = items.Find(x => x.BorrowedBy.MemberShipNumber == code);
        if (borrowingRecord != null)
        {
            return items.IndexOf(borrowingRecord);
        }
        else
        {
            return -1;
        }
    }

}
