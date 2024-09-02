using LMS.Business.Abstracts;
using LMS.DataAccess.Entities;

namespace LMS.Business.Managers;

public class ReserveManager : ManagerBase<Reserve, string>
{
    public Reserve? FindReserve(Book book)
    {
        var reserve = items.FindLast(r => r.ReservedBook == book);
        return reserve;
    }

    protected override int ReturnIndex(string code)
    {
        var reserve = items.Find(x => x.ReservedBy.MemberShipNumber == code);
        if (reserve != null)
        {
            return items.IndexOf(reserve);
        }
        else
        {
            return -1;
        }
    }
}
