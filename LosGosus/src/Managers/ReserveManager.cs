public class ReserveManager
{
    private List<Reserve> ReserveRecords;

    public ReserveManager() {
        ReserveRecords = new List<Reserve>();
    }

    public void AddReserve(Reserve reserve) {
        ReserveRecords.Add(reserve);
    }

    public Reserve? FindReserve(Book book) {
        var reserve = ReserveRecords.FindLast(r => r.ReservedBook == book);
        return reserve;
    }

    public void ListReserves()
    {
        Paginator.Paginate<Reserve>(ReserveRecords);
    }
}
