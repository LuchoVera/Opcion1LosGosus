using System.Text.RegularExpressions;

public class PatronValidator : IValidator<Patron>
{

    private const int maxCharactersLength = 50;
    private const int minCharactersLength = 1;
    protected static bool ValidateName(string name)
    {
        return ValidateNotNullOrEmpty(name);
    }
    protected static bool ValidateMemberShipNumber(string memberShipNumber) {
        return !string.IsNullOrEmpty(memberShipNumber) && memberShipNumber.StartsWith("ptr-") && memberShipNumber.Length == 14 && memberShipNumber.Substring(4).All(char.IsDigit);
    }
    protected static bool ContactDetails(string contactDetails)
    {
        return ValidateNotNullOrEmpty(contactDetails);
    }

    public bool Validate(Patron patron)
    {
        return ValidateName(patron.Name) &&
               ValidateMemberShipNumber(patron.MemberShipNumber) &&
               ContactDetails(patron.ContactDetails);
    }

    protected static bool ValidateNotNullOrEmpty(string value)
    {
        return !string.IsNullOrEmpty(value) && value.Length <= maxCharactersLength && value.Length >= minCharactersLength;
    }
}
