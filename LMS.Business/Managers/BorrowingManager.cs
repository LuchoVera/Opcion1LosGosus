using LMS.Business.Abstracts;
using LMS.Business.Services;
using LMS.DataAccess.Entities;

namespace LMS.Business.Managers;

public class BorrowingManager : ManagerBase<BorrowingRecord, string>
{

    public BorrowingRecord? FindBorrowingRecord(Patron patron, Book book)
    {
        var record = items.Find(r => r.BorrowedBook == book && r.BorrowedBy == patron && !r.ReturnDate.HasValue);
        return record;
    }

    public string GetOverdueBooks()
    {
        var overdueBooks = items
            .Where(FineCalculator.IsOverdue)
            .Select(record => record.ToString());

        return string.Join(Environment.NewLine, overdueBooks);
    }

    public string GetBorrowingHistory(string membershipNumber)
    {
        var history = items
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
