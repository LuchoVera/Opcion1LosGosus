using LMS.Business.Interfaces;
using LMS.Business.Services;

namespace LMS.Business.Abstracts;

public abstract class ManagerBase<T, C> : IManager<T, C>
{
    protected List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public void Update(T item, C code)
    {
        items[ReturnIndex(code)] = item;
    }

    public void Delete(C code)
    {
        items.RemoveAt(ReturnIndex(code));
    }

    public void List()
    {
        Paginator.Paginate<T>(items);

    }

    protected abstract int ReturnIndex(C code);
}
