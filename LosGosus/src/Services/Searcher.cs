public class Searcher<T>
{
    private ISearch<T> _searchStrategy;

    public Searcher(ISearch<T> searchStrategy)
    {
        _searchStrategy = searchStrategy;
    }

    public List<T> SearchMultiple(List<T> items, Func<T, bool> predicate)
    {
        return _searchStrategy.SearchMultiple(items, predicate);
    }

    public T? SearchSingle(List<T> items, Func<T, bool> predicate)
    {
        return _searchStrategy.SearchSingle(items, predicate);
    }
}
