public interface ISearch<T>
{
    List<T> SearchMultiple(List<T> items, Func<T, bool> predicate);
    
    T? SearchSingle(List<T> items, Func<T, bool> predicate);
}
