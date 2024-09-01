using LosGosus.Interfaces;
using LosGosus.Models;

namespace LosGosus.Services;

public class PatronSearcher : ISearch<Patron>
{
    public List<Patron> SearchMultiple(List<Patron> items, Func<Patron, bool> predicate)
    {
        return items.Where(predicate).ToList();
    }

    public Patron? SearchSingle(List<Patron> items, Func<Patron, bool> predicate)
    {
        return items.FirstOrDefault(predicate);
    }
}
