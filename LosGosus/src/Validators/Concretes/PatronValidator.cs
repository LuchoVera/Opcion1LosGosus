using LosGosus.Models;
using LosGosus.Validators.Bases;

namespace LosGosus.Validators.Concretes;

public sealed class PatronValidator() : BaseValidator<Patron>(50)
{
    private bool ValidateName(string name)
    {
        return ValidateNotNullOrEmpty(name)
            && ValidateLength(name)
            && ValidateStringLettersWithSpaces(name);
    }

    private bool ValidateMemberShipNumber(string memberShipNumber) {
        return !string.IsNullOrEmpty(memberShipNumber) 
            && memberShipNumber.StartsWith("ptr-") 
            && memberShipNumber.Length == 14 
            && memberShipNumber.Substring(4).All(char.IsDigit);
    }

    private bool ContactDetails(string contactDetails)
    {
        return ValidateNotNullOrEmpty(contactDetails)
            && ValidateLength(contactDetails);
    }

    protected override IList<Func<Patron, bool>> ValidationResults()
    {
        return new List<Func<Patron, bool>>
        {
            patron => ValidateName(patron.Name),
            patron => ValidateMemberShipNumber(patron.MemberShipNumber),
            patron => ContactDetails(patron.ContactDetails),
        };
    }

    public new bool Validate(Patron patron)
    {
        foreach (Func<Patron, bool> rule in ValidationResults())
        {
            if (!rule(patron))
            {
                return false;
            }
        }
        return true;
    }

}
