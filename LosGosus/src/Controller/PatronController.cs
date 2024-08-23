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

    public void DeletePatron(string memberShipNumber)
    {
        patronManager.Delete(memberShipNumber);
    }

    public void ListPatrons()
    {
        patronManager.List();
    }

    public void ShowPatronByMembershipNumber(string membershipNumber)
    {
        patronManager.ShowPatronByMembershipNumber(membershipNumber);
    }

    public void ShowPatronByName(string name)
    {
        patronManager.ShowPatronByName(name);
    }

    public PatronManager GetPatronManager()
    {
        return patronManager;
    }
}
