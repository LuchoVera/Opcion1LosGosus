public class PatronManager : ManagerBase<Patron, string>
{
    private PatronSearcher? patronSearcher;

    public PatronManager()
    {
        patronSearcher = new PatronSearcher(items);
    }

    public void ShowPatronByMembershipNumber(string membershipNumber) 
    {
        if (patronSearcher != null)
        {
            var patron = patronSearcher.SearchByMemberShipNumber(membershipNumber);
            Console.WriteLine(patron);
        }
    }

    public void ShowPatronByName(string name) 
    {
        if (patronSearcher != null)
        {
            var patrons = patronSearcher.SearchByName(name);
            Paginator.Paginate<Patron>(patrons);
        }
    }

    protected override int ReturnIndex(string membershipNumber)
    {
        var patron = items.Find(x => x.MemberShipNumber == membershipNumber);
        if (patron != null)
        {
            return items.IndexOf(patron);
        }
        else
        {
            return -1;
        }
    }

    public Patron? GetPatronByMembershipNumber(string membershipNumber)
    {
        return items.Find(x => x.MemberShipNumber == membershipNumber);
    }
    public List<Patron> GetPatrons()
    {
        return items;
    }

}
