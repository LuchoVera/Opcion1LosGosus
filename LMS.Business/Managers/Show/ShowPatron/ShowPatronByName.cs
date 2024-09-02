using LMS.Business.Services;
using LMS.DataAccess.Entities;
using LMS.DataAccess.Interfaces;

namespace LMS.Business.Managers.Show.ShowPatron;

public class ShowPatronByName : IShow<Patron>
{
    private Searcher<Patron> patronSearcher;

    public ShowPatronByName()
    {
        patronSearcher = new Searcher<Patron>(new PatronSearcher());
    }
    public void ShowResult(List<Patron> items, string criteria)
    {
        if (patronSearcher != null)
        {
            var patrons = patronSearcher.SearchMultiple(items, patron => patron.Name.Equals(criteria, StringComparison.OrdinalIgnoreCase));
            Paginator.Paginate<Patron>(patrons);
        }
    }
}