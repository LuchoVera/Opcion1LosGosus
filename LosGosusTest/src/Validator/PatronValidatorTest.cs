public class PatronValidatorTests
{
    private readonly PatronValidator _validator;

    public PatronValidatorTests()
    {
        _validator = new PatronValidator();
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameIsEmpty()
    {
        Patron patron = new Patron("", "ptr-202400123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameHasOnlySpaces()
    {
        Patron patron = new Patron("   ", "ptr-202400123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberIsInvalidFormat()
    {
        Patron patron = new Patron("Carlos Valverde", "ptr-abcde1234567", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberDoesNotStartWithPtr()
    {
        Patron patron = new Patron("Carlos Valverde", "mem-202400123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberHasNonDigitCharactersAfterPrefix()
    {
        Patron patron = new Patron("Carlos Valverde", "ptr-202400abc123", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsAreEmpty()
    {
        Patron patron = new Patron("Carlos Valverde", "ptr-202400123456", "");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameExceedsMaxLength()
    {
        string longName = new string('A', 51);
        Patron patron = new Patron(longName, "ptr-202400123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsExceedMaxLength()
    {
        string longContactDetails = new string('B', 51);
        Patron patron = new Patron("Carlos Valverde", "ptr-202400123456", longContactDetails);
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberContainsSpaces()
    {
        Patron patron = new Patron("Carlos Valverde", "ptr-20240 0123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsContainsInvalidCharacters()
    {
        Patron patron = new Patron("Carlos Valverde", "ptr-202400123456", "Calle Murillo, La Paz!");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameContainsInvalidCharacters()
    {
        Patron patron = new Patron("Carlos Valverde!", "ptr-202400123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameIsBelowMinLength()
    {
        string shortName = "";
        Patron patron = new Patron(shortName, "ptr-202400123456", "Calle Murillo, La Paz");
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsIsBelowMinLength()
    {
        string shortContactDetails = "";
        Patron patron = new Patron("Carlos Valverde", "ptr-202400123456", shortContactDetails);
        bool result = _validator.Validate(patron);
        Assert.False(result);
    }
}
