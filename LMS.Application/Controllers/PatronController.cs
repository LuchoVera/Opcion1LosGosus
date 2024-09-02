using LMS.Business.Managers;
using LMS.DataAccess.Entities;
using LMS.DataAccess.Validators;

namespace LMS.Application.Controllers;

public class PatronController
{
    private PatronManager patronManager = new PatronManager();
    private PatronValidator patronValidator = new PatronValidator();

    public bool TryAddPatron(string name, string memberShipNumber, string contactDetails)
    {
        Patron patron = new Patron(name, memberShipNumber, contactDetails);
        if (patronValidator.Validate(patron))
        {
            patronManager.Add(patron);
            return true;
        }
        return false;
    }

    public bool TryUpdatePatron(string name, string memberShipNumber, string contactDetails)
    {
        Patron patron = new Patron(name, memberShipNumber, contactDetails);
        if (patronValidator.Validate(patron))
        {
            patronManager.Update(patron, memberShipNumber);
            return true;
        }
        return false;
    }

    public bool DeletePatron(string memberShipNumber)
    {
        if (patronManager.SearchPatron(patron => patron.MemberShipNumber
        .Equals(memberShipNumber, StringComparison.OrdinalIgnoreCase)) == null)
        {
            return false;    
        }
        patronManager.Delete(memberShipNumber);
        return true;
    }

    public void ListPatrons()
    {
        patronManager.List();
    }

    public PatronManager GetPatronManager()
    {
        return patronManager;
    }
}
