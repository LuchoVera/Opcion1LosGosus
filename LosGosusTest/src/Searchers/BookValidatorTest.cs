public class BookValidatorTests
{
    private readonly BookValidator validator = new BookValidator();

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidTitle()
    {
        Book validBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(validBook);
        Assert.True(validationResults["Title"]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("This title is way too long and should fail because it exceeds the maximum allowed length of 100 characters.")]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidTitle(string invalidTitle)
    {
        Book invalidBook = new Book(invalidTitle, "Valid Author", "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(invalidBook);
        Assert.False(validationResults["Title"]);
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidAuthor()
    {
        Book validBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(validBook);
        Assert.True(validationResults["Author"]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("This author's name is way too long and should fail because it exceeds the maximum allowed length of 100 characters.")]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidAuthor(string invalidAuthor)
    {
        Book invalidBook = new Book("Valid Title", invalidAuthor, "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(invalidBook);
        Assert.False(validationResults["Author"]);
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidISBN()
    {
        Book validBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(validBook);
        Assert.True(validationResults["ISBN"]);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("isbn-123")]
    [InlineData("isbn-12345abcde")]
    [InlineData("1234567890")]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidISBN(string invalidISBN)
    {
        Book invalidBook = new Book("Valid Title", "Valid Author", invalidISBN, "Fiction", 10);
        var validationResults = validator.GetValidationResults(invalidBook);
        Assert.False(validationResults["ISBN"]);
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidGenre()
    {
        Book validBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(validBook);
        Assert.True(validationResults["Genre"]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("This genre is way too long and should fail because it exceeds the maximum allowed length of 100 characters.")]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidGenre(string invalidGenre)
    {
        Book invalidBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", invalidGenre, 10);
        var validationResults = validator.GetValidationResults(invalidBook);
        Assert.False(validationResults["Genre"]);
    }

    [Fact]
    public void GetValidationResults_ShouldReturnTrue_ForValidPublicationYear()
    {
        Book validBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 10);
        var validationResults = validator.GetValidationResults(validBook);
        Assert.True(validationResults["PublicationYear"]);
    }

    [Fact]
    public void GetValidationResults_ShouldReturnFalse_ForInvalidPublicationYear()
    {
        Book invalidBook = new Book("Valid Title", "Valid Author", "isbn-1000000000", "Fiction", 465454654);
        var validationResults = validator.GetValidationResults(invalidBook);
        Assert.False(validationResults["PublicationYear"]);
    }
}
