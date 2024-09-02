using LMS.Application.Controllers;

namespace LMS.Application.Tests.Controllers;

public class PatronControllerTest
{
    [Fact]
    public void TryAddPatron_ShouldReturnTrue_WhenValidationSucceeds()
    {
        var controller = new PatronController();
        var memberShipNumber = "ptr-1234567890";
        bool result = controller.TryAddPatron("John Doe", "ptr-1234567890", "john.doe@example.com");

        Assert.True(result);

        var addedPatron = controller.GetPatronManager().SearchPatron(patron => patron.MemberShipNumber
        .Equals(memberShipNumber, StringComparison.OrdinalIgnoreCase));
        Assert.NotNull(addedPatron);
        Assert.Equal("John Doe", addedPatron.Name);
        Assert.Equal("john.doe@example.com", addedPatron.ContactDetails);
    }

    [Fact]
    public void TryAddPatron_ShouldReturnFalse_WhenValidationFails()
    {
        var controller = new PatronController();

        bool result = controller.TryAddPatron("John Doe", "", "john.doe@example.com");

        Assert.False(result);
    }

    [Fact]
    public void TryUpdatePatron_ShouldReturnTrue_WhenValidationSucceeds()
    {
        var controller = new PatronController();
        var memberShipNumber = "ptr-1234567890";
        controller.TryAddPatron("John Doe", "ptr-1234567890", "john.doe@example.com");

        bool result = controller.TryUpdatePatron("Jane Doe", "ptr-1234567890", "jane.doe@example.com");

        Assert.True(result);

        var updatedPatron = controller.GetPatronManager().SearchPatron(patron => patron.MemberShipNumber
        .Equals(memberShipNumber, StringComparison.OrdinalIgnoreCase));
        Assert.NotNull(updatedPatron);
        Assert.Equal("Jane Doe", updatedPatron.Name);
        Assert.Equal("jane.doe@example.com", updatedPatron.ContactDetails);
    }

    [Fact]
    public void TryUpdatePatron_ShouldReturnFalse_WhenValidationFails()
    {
        var controller = new PatronController();

        controller.TryAddPatron("John Doe", "ptr-1234567890", "john.doe@example.com");

        bool result = controller.TryUpdatePatron("", "ptr-1234567890", "jane.doe@example.com");

        Assert.False(result);
    }

    [Fact]
    public void DeletePatron_ShouldReturnTrue_WhenPatronIsDeleted()
    {
        var controller = new PatronController();
        var memberShipNumber = "ptr-1234567890";
        controller.TryAddPatron("John Doe", "ptr-1234567890", "john.doe@example.com");

        bool result = controller.DeletePatron("ptr-1234567890");

        Assert.True(result);

        var deletedPatron = controller.GetPatronManager().SearchPatron(patron => patron.MemberShipNumber
        .Equals(memberShipNumber, StringComparison.OrdinalIgnoreCase));
        Assert.Null(deletedPatron);
    }

    [Fact]
    public void DeletePatron_ShouldReturnFalse_WhenPatronStillExists()
    {
        var controller = new PatronController();

        controller.TryAddPatron("John Doe", "ptr-1234567890", "john.doe@example.com");

        bool result = controller.DeletePatron("ptr-9999999999");

        Assert.False(result);
    }

}
