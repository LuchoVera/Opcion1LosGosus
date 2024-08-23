public class BorrowingManager : ManagerBase<BorrowingRecord, string>{

    public void AddBorrowingRecord(BorrowingRecord record) {
        items.Add(record);
    }

    public BorrowingRecord? FindBorrowingRecord(Patron patron, Book book) {
        var record = items.Find(r => r.BorrowedBook == book && r.BorrowedBy == patron && !r.ReturnDate.HasValue);
        return record;
    }

    public string GetOverdueBooks()
    {
        var overdueBooks = items
            .Where(record => record.IsOverdue())
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, overdueBooks);
    }

    public string GetBorrowingHistory(string memberSHipNumber)
    {
        var history = items
            .Where(record => record.BorrowedBy.MemberShipNumber == memberSHipNumber)
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
