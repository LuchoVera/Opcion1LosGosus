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
        string content = value.Trim();
        
        if (!ContainsOnlyValidCharacters(content))
        {
            return false;
        }
        
        if (!AreHyphensCorrectlyPlaced(content))
        {
            return false;
        }

        return true;
    }
    
    private bool ContainsOnlyValidCharacters(string value)
    {
        return value.All(static c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-');
    }
    
    private bool AreHyphensCorrectlyPlaced(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] == '-')
            {
                if (i == 0 || i == value.Length - 1)
                {
                    return false;
                }
                
                char before = value[i - 1];
                char after = value[i + 1];

                if (!((char.IsLetter(before) || char.IsWhiteSpace(before)) &&
                      (char.IsLetter(after) || char.IsWhiteSpace(after))))
                {
                    return false;
                }
            }
        }
        return true;
    }

    protected bool ValidateYear(int year)
    {
        return year >= 0 && year <= DateTime.Now.Year;
    }

    protected abstract IList<Func<T, bool>> ValidationResults();

    public abstract bool Validate(T item);
}
