namespace LosGosus.Managers.Show;

public interface IShow <T>
{
    void ShowResult(List<T> items, string criteria);
}
