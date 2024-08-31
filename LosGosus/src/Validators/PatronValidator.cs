using System.Text.RegularExpressions;

public class PatronValidator : IValidator<Patron>
{

    private const int maxCharactersLength = 50;
    private const int minCharactersLength = 1;
    protected static bool ValidateMemberShipNumber(string memberShipNumber) {
        return !string.IsNullOrEmpty(memberShipNumber) && memberShipNumber.StartsWith("ptr-") && memberShipNumber.Length == 14 && memberShipNumber.Substring(4).All(char.IsDigit);
    }

    public bool Validate(Patron patron)
    {
        return ValidateNotNullOrEmpty(patron.Name) &&
               ValidateMemberShipNumber(patron.MemberShipNumber) &&
               ValidateNotNullOrEmpty(patron.ContactDetails);
    }

    protected static bool ValidateNotNullOrEmpty(string value)
    {
        return !string.IsNullOrEmpty(value) && value.Length <= maxCharactersLength && value.Length >= minCharactersLength;
    }
}
