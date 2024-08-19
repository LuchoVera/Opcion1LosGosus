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
                        "4. Go back"
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
                case "4. Go back":
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
        string patronId = Generator.GeneratePRTId();
        string membershipNumber = Generator.GeneratePTRMemberNum();
        string contactDetails = AnsiConsole.Ask<string>("Contact Details:");

        bool success = patronController.TryAddPatron(name, patronId, membershipNumber, contactDetails);

        if (success)
        {
            AnsiConsole.MarkupLine("[green]Patron added successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Patron is not valid. Please check the details and try again.[/]");
        }

        Pause();
    }

    private void UpdatePatron()
    {
        AnsiConsole.MarkupLine("[bold yellow]Enter Patron Details:[/]");
        string name = AnsiConsole.Ask<string>("Name:");
        string patronId = AnsiConsole.Ask<string>("Patron ID:");
        string membershipNumber = AnsiConsole.Ask<string>("Membership Number:");
        string contactDetails = AnsiConsole.Ask<string>("Contact Details:");

        bool success = patronController.TryUpdatePatron(name, patronId, membershipNumber, contactDetails);

        if (success)
        {
            AnsiConsole.MarkupLine("[green]Patron updated successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Patron is not valid. Please check the details and try again.[/]");
        }

        Pause();
    }

    private void DeletePatron()
    {
        string patronId = AnsiConsole.Ask<string>("Enter Patron ID:");
        patronController.DeletePatron(patronId);
        AnsiConsole.MarkupLine("[green]Patron deleted successfully![/]");
        Pause();
    }

    private void ListPatrons()
    {
        AnsiConsole.MarkupLine("[bold yellow]Listing all patrons:[/]");
        patronController.ListPatrons();
        Pause();
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
