using LosGosus.Models;
using LosGosus.Validators.Concretes;

public class BookValidatorTests
{
    private readonly BookValidator _validator = new();

    public BookValidatorTests()
    {
        _validator = new BookValidator();
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidTitle()
    {
        Book validBook = new("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);

        Assert.True(_validator.Validate(validBook));
    }

    [Theory]
    [InlineData(null)]
    [InlineData(
        "This title is way too long and should fail because it exceeds the maximum allowed length of 100 characters."
    )]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidTitle(string invalidTitle)
    {
        Book invalidBook = new(invalidTitle, "Valid Author", "isbn-1000000000", "Fiction", 10);

        Assert.False(_validator.Validate(invalidBook));
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidAuthor()
    {
        Book validBook = new("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);

        Assert.True(_validator.Validate(validBook));
    }

    [Theory]
    [InlineData(null)]
    [InlineData(
        "This author's name is way too long and should fail because it exceeds the maximum allowed length of 100 characters."
    )]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidAuthor(string invalidAuthor)
    {
        Book invalidBook = new("Valid Title", invalidAuthor, "isbn-1000000000", "Fiction", 10);

        Assert.False(_validator.Validate(invalidBook));
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidISBN()
    {
        Book validBook = new("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);

        Assert.True(_validator.Validate(validBook));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("isbn-123")]
    [InlineData("isbn-12345abcde")]
    [InlineData("1234567890")]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidISBN(string invalidISBN)
    {
        Book invalidBook = new("Valid Title", "Valid Author", invalidISBN, "Fiction", 10);

        Assert.False(_validator.Validate(invalidBook));
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidGenre()
    {
        Book validBook = new("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);

        Assert.True(_validator.Validate(validBook));
    }

    [Theory]
    [InlineData(null)]
    [InlineData(
        "This genre is way too long and should fail because it exceeds the maximum allowed length of 100 characters."
    )]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidGenre(string invalidGenre)
    {
        Book invalidBook = new("Valid Title", "Valid Author", "isbn-1000000000", invalidGenre, 10);

        Assert.False(_validator.Validate(invalidBook));
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidPublicationYear()
    {
        Book validBook = new("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);

        Assert.True(_validator.Validate(validBook));
    }

    [Fact]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidPublicationYear()
    {
        Book invalidBook =
            new("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 465454654);

        Assert.False(_validator.Validate(invalidBook));
    }
}
