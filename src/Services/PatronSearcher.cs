public class PatronSearcher
{
    private List<Patron> _patrons = new List<Patron>();
    
    public PatronSearcher(List<Patron> patrons)
    {
        _patrons = patrons;
    }

    public List<Patron> SearchByName(string name)
    {
        return SearchBy(x => x.Name == name);
    }
    public Patron SearchByMemberShipNumber(string mememberShipNumber)
    { 
        var patron = _patrons.Find(x => x.MemberShipNumber == mememberShipNumber);
        return patron != null ? patron : throw new Exception("Patron not found");

    }

    public List<Patron> SearchBy(Predicate<Patron> predicate)
    {
        return _patrons.FindAll(predicate);
    }    
}
