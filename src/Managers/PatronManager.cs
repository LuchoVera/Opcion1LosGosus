public class PatronManager : IManager<Patron, string> 
{
    private List<Patron> patrons = new List<Patron>();
    private PatronSearcher? patronSearcher;

    public PatronManager()
    {
        patronSearcher = new PatronSearcher(patrons);
    }
    public void Add(Patron patron) 
    {
        patrons.Add(patron);
    }

    public void Update(Patron patron, string patronId) 
    {
        patrons[ReturnIndex(patronId)] = patron;
    }

    public void Delete(string patronId) 
    {
        patrons.RemoveAt(ReturnIndex(patronId));
    }

    public void List() 
    {
        foreach (Patron patron in patrons) 
        {
            Console.WriteLine(patron);
        }
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
            foreach (var patron in patrons)
            {
                Console.WriteLine(patron);
            }
        }
    }

    private int ReturnIndex(string patronId)
    {
        var patron = patrons.Find(x => x.PatronId == patronId);
        if (patron != null)
        {
            return patrons.IndexOf(patron);
        }
        else
        {
            return -1;
        }
    }

    public Patron? GetPatronById(string patronId)
    {
        return patrons.Find(x => x.PatronId == patronId);
    }
}
