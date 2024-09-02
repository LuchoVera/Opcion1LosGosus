namespace LMS.DataAccess.Interfaces;

public interface IValidator<T>
{
    bool Validate(T item);
}
