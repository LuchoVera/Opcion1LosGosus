using LosGosus.Models;
using LosGosus.Validators.Concretes;
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
        Patron patron = new("", "ptr-202400123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameHasOnlySpaces()
    {
        Patron patron = new("   ", "ptr-202400123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberIsInvalidFormat()
    {
        Patron patron = new("Carlos Valverde", "ptr-abcde1234567", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberDoesNotStartWithPtr()
    {
        Patron patron = new("Carlos Valverde", "mem-202400123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberHasNonDigitCharactersAfterPrefix()
    {
        Patron patron = new("Carlos Valverde", "ptr-202400abc123", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsAreEmpty()
    {
        Patron patron = new("Carlos Valverde", "ptr-202400123456", "");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameExceedsMaxLength()
    {
        string longName = new('A', 51);
        Patron patron = new(longName, "ptr-202400123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsExceedMaxLength()
    {
        string longContactDetails = new('B', 51);
        Patron patron = new("Carlos Valverde", "ptr-202400123456", longContactDetails);

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenMemberShipNumberContainsSpaces()
    {
        Patron patron = new("Carlos Valverde", "ptr-20240 0123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsContainsInvalidCharacters()
    {
        Patron patron = new("Carlos Valverde", "ptr-202400123456", "Calle Murillo, La Paz!");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameContainsInvalidCharacters()
    {
        Patron patron = new("Carlos Valverde!", "ptr-202400123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenNameIsBelowMinLength()
    {
        string shortName = "";
        Patron patron = new(shortName, "ptr-202400123456", "Calle Murillo, La Paz");

        Assert.False(_validator.Validate(patron));
    }

    [Fact]
    public void Validate_ShouldReturnFalse_WhenContactDetailsIsBelowMinLength()
    {
        string shortContactDetails = "";
        Patron patron = new("Carlos Valverde", "ptr-202400123456", shortContactDetails);

        Assert.False(_validator.Validate(patron));
    }
}
