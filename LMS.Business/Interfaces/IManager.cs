namespace LMS.Business.Interfaces;

public interface IManager<T, C> {
    void Add(T item);
    void Update(T item, C code);
    void Delete(C code);
    void List();
}
