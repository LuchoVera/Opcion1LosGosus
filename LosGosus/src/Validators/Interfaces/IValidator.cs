namespace LosGosus.Validators.Interfaces;

public interface IValidator<T>
{
    bool Validate(T item);
}
