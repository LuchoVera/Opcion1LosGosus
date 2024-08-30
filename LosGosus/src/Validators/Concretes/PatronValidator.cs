public sealed class PatronValidator() : BaseValidator<Patron>(50)
{
    private bool ValidateName(string name)
    {
        return ValidateNotNullOrEmpty(name)
            && ValidateLength(name)
            && ValidateStringLettersWithSpaces(name);
    }

    private bool ValidateMemberShipNumber(string memberShipNumber)
    {
        return ValidateNotNullOrEmpty(memberShipNumber)
            && memberShipNumber.StartsWith("ptr-", StringComparison.Ordinal)
            && memberShipNumber.Length == 14
            && memberShipNumber[4..].All(char.IsDigit);
    }

    private bool ContactDetails(string contactDetails)
    {
        return ValidateNotNullOrEmpty(contactDetails)
            && ValidateLength(contactDetails)
            && contactDetails.All(static c =>
                c == '@' || c == '.' || c == ',' || char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)
            );
    }

    protected override IList<Func<Patron, bool>> ValidationResults()
    {
        return
        [
            patron => ValidateName(patron.Name),
            patron => ValidateMemberShipNumber(patron.MemberShipNumber),
            patron => ContactDetails(patron.ContactDetails),
        ];
    }

    
    public new bool Validate(Patron patron) {
        var validationResults = ValidationResults();
        return validationResults.All(func => func(patron));
    }
}
