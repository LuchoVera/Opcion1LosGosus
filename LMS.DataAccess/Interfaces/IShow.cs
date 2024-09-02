namespace LMS.DataAccess.Interfaces;

public interface IShow <T>
{
    void ShowResult(List<T> items, string criteria);
}
