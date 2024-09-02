using LMS.Business.Abstracts;
using LMS.Business.Managers.Show;
using LMS.Business.Managers.Show.ShowPatron;
using LMS.Business.Services;
using LMS.DataAccess.Entities;

namespace LMS.Business.Managers;

public class PatronManager : ManagerBase<Patron, string>
{
    private Searcher<Patron> patronSearcher;
    private ShowContext<Patron> show;

    public PatronManager()
    {
        show = new ShowContext<Patron>();
        patronSearcher = new Searcher<Patron>(new PatronSearcher());
    }

    public void ShowPatronByMembershipNumber(string membershipNumber)
    {
        show.SetShow(new ShowPatronByMembershipNumber());
        show.Execute(items, membershipNumber);
    }

    public void ShowPatronByName(string name)
    {
        show.SetShow(new ShowPatronByName());
        show.Execute(items, name);
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
}
