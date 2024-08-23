
public class BorrowingManager : ManagerBase<BorrowingRecord, string>
{
    private List<BorrowingRecord> borrowingRecords;
    private const double CostPerDay = 0.5;

    public BorrowingManager()
    {
        borrowingRecords = new List<BorrowingRecord>();
    }


    public void AddBorrowingRecord(BorrowingRecord record)
    {
        items.Add(record);
    }

    public BorrowingRecord? FindBorrowingRecord(Patron patron, Book book)
    {
        var record = items.Find(r => r.BorrowedBook == book && r.BorrowedBy == patron && !r.ReturnDate.HasValue);
        return record;
    }

    public string GetOverdueBooks()
    {
        var overdueBooks = borrowingRecords
            .Where(IsOverdue)
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, overdueBooks);
    }

    public bool IsOverdue(BorrowingRecord record)
    {
        if (record.ReturnDate.HasValue)
        {
            return false;
        }
        return DateTime.Now > record.DueDate;
    }

    public double CalculateFine(BorrowingRecord record)
    {
        if (record.ReturnDate.HasValue)
        {
            return 0.0;
        }

        if (IsOverdue(record))
        {
            return 0.0;
        }

        int overdueDays = (DateTime.Now - record.DueDate).Days;
        return overdueDays * CostPerDay;
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
