using LosGosus.Models;

namespace LosGosusTest.ModelsTest;

public class PatronTest
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        string name = "John Doe";
        string membershipNumber = "12345";
        string contactDetails = "john.doe@example.com";

        Patron patron = new Patron(name, membershipNumber, contactDetails);

        Assert.Equal(name, patron.Name);
        Assert.Equal(membershipNumber, patron.MemberShipNumber);
        Assert.Equal(contactDetails, patron.ContactDetails);
        Assert.Empty(patron.BorrowingRecords);
        Assert.Empty(patron.ReservedRecords);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        Patron patron = new Patron("John Doe", "12345", "john.doe@example.com");

        string expectedOutput = "Name: John Doe, Contact Details: john.doe@example.com\n" +
                                "Membership Number: 12345\n";

        string result = patron.ToString();

        Assert.Equal(expectedOutput, result);
    }

    [Fact]
    public void BorrowingRecords_ShouldBeEmptyInitially()
    {
        Patron patron = new Patron("John Doe", "12345", "john.doe@example.com");

        Assert.Empty(patron.BorrowingRecords);
    }

    [Fact]
    public void ReservedRecords_ShouldBeEmptyInitially()
    {
        Patron patron = new Patron("John Doe", "12345", "john.doe@example.com");

        Assert.Empty(patron.ReservedRecords);
    }
}
