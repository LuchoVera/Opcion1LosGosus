using Spectre.Console;

public class PatronManagerUI
{
    private PatronController patronController;
    private PatronSearcherUI patronSearcherUI;

    public PatronManagerUI(PatronController _patronController)
    {
        patronController = _patronController;
        patronSearcherUI = new PatronSearcherUI(patronController.GetPatronManager());
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Patron Management[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Add Patron",
                        "2. Update Patron",
                        "3. Delete Patron",
                        "4. List Patrons",
                        "5. Search Patrons",
                        "0. Go back"
                    }));

            switch (choice)
            {
                case "1. Add Patron":
                    AddPatron();
                    break;
                case "2. Update Patron":
                    UpdatePatron();
                    break;
                case "3. Delete Patron":
                    DeletePatron();
                    break;
                case "4. List Patrons":
                    ListPatrons();
                    break;
                case "5. Search Patrons":
                    SearchPatrons();
                    break;
                case "0. Go back":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    break;
            }
        }
    }

    private void AddPatron()
    {
        AnsiConsole.MarkupLine("[bold yellow]Enter Patron Details:[/]");
        string name = AnsiConsole.Ask<string>("Name:");
        string membershipNumber = Generator.GeneratePTRMemberNum();
        string contactDetails = AnsiConsole.Ask<string>("Contact Details:");

        bool success = patronController.TryAddPatron(name, membershipNumber, contactDetails);

        if (success)
        {
            AnsiConsole.MarkupLine("[green]Patron added successfully![/]");
        }
        else
        {
            ErrorHandler.HandleError(new InvalidPatronException("Patron is not valid. Please check the information and try again."));
        }

        Pause();
    }

    private void UpdatePatron()
    {
        AnsiConsole.MarkupLine("[bold yellow]Enter Patron Details:[/]");
        string name = AnsiConsole.Ask<string>("Name:");
        string membershipNumber = AnsiConsole.Ask<string>("Membership Number:");
        string contactDetails = AnsiConsole.Ask<string>("Contact Details:");

        bool success = patronController.TryUpdatePatron(name, membershipNumber, contactDetails);

        if (success)
        {
            AnsiConsole.MarkupLine("[green]Patron updated successfully![/]");
        }
        else
        {
            ErrorHandler.HandleError(new InvalidPatronException("Invalid Patron information."));
        }

        Pause();
    }

    private void DeletePatron()
    {
        string membershipNumber = AnsiConsole.Ask<string>("Enter Patron Membership Number:");
        try
        {
            if (patronController.DeletePatron(membershipNumber))
            {
                AnsiConsole.MarkupLine("[green]Patron deleted successfully![/]");
            } else
            {
                AnsiConsole.MarkupLine("[red]Error deleting the patron![/]");

            }
        }
        catch (Exception)
        {
            ErrorHandler.HandleError(new InvalidPatronException("Error deleting the patron"));
        }
        Pause();
    }

    private void ListPatrons()
    {
        AnsiConsole.MarkupLine("[bold yellow]Listing all patrons:[/]");
        patronController.ListPatrons();
   
    }

    private void SearchPatrons()
    {
        patronSearcherUI.Run();
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
