public class PatronManager : ManagerBase<Patron, string>
{
    private Searcher<Patron> patronSearcher;

    public PatronManager()
    {
        patronSearcher = new Searcher<Patron>(new PatronSearcher());
    }

    public void ShowPatronByMembershipNumber(string membershipNumber)
    {
        if (patronSearcher != null)
        {
            var patron = SearchPatron(patron => patron.MemberShipNumber.Equals(membershipNumber, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(patron);
        }
    }

    public void ShowPatronByName(string name)
    {
        if (patronSearcher != null)
        {
            var patrons = SearchPatrons(patron => patron.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            Paginator.Paginate<Patron>(patrons);
        }
    }

    public List<Patron> SearchPatrons(Func<Patron, bool> predicate)
    {
        return patronSearcher.SearchMultiple(items, predicate);
    }

    public Patron? SearchPatron(Func<Patron, bool> predicate)
    {
        return patronSearcher.SearchSingle(items, predicate);
    }

    protected override int ReturnIndex(string membershipNumber)
    {
        var patron = items.Find(x => x.MemberShipNumber == membershipNumber);
        return patron != null ? items.IndexOf(patron) : -1;
    }

    public List<Patron> GetPatrons()
    {
        return items;
    }

    public Patron? GetPatronByMembershipNumber(string membershipNumber)
    {
        return items.Find(x => x.MemberShipNumber == membershipNumber);
    }

}
