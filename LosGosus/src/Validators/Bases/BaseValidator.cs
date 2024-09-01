using LosGosus.Validators.Interfaces;

namespace LosGosus.Validators.Bases;

public abstract class BaseValidator<T>(int maxCharactersLength, int minCharactersLength = 1)
    : IValidator<T>
{
    protected int MinCharactersLength { get; } = minCharactersLength;
    protected int MaxCharactersLength { get; } = maxCharactersLength;

    protected bool ValidateNotNullOrEmpty(string value)
    {
        return !string.IsNullOrEmpty(value);
    }

    protected bool ValidateLength(string value)
    {
        return value.Length <= MaxCharactersLength && value.Length >= MinCharactersLength;
    }

    protected bool ValidateStringLettersWithSpaces(string value)
    {
        return value.All(static c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

    protected bool ValidateYear(int year)
    {
        return year >= 0 && year <= DateTime.Now.Year;
    }

    protected abstract IList<Func<T, bool>> ValidationResults();

    public abstract bool Validate(T item);
}
