using LMS.Business.Services;
using LMS.DataAccess.Entities;
using LMS.DataAccess.Interfaces;

namespace LMS.Business.Managers.Show.ShowPatron;

public class ShowPatronByMembershipNumber : IShow<Patron>
{
   private Searcher<Patron> patronSearcher;

    public ShowPatronByMembershipNumber()
    {
        patronSearcher = new Searcher<Patron>(new PatronSearcher());
    }
    public void ShowResult(List<Patron> items, string criteria)
    {
        if (patronSearcher != null)
        {
            var patron = patronSearcher.SearchSingle(items, patron => patron.MemberShipNumber.Equals(criteria, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(patron);
        }
    }
}
