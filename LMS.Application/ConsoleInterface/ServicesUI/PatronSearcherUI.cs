using LMS.Business.Managers;
using LMS.Business.Services.ErrorHandler;
using LMS.Business.Services.ErrorHandler.Exceptions;

using Spectre.Console;

namespace LMS.Application.ConsoleInterface.ServicesUI;

public class PatronSearcherUI
{
    private PatronManager patronManager;

    public PatronSearcherUI(PatronManager patronManager)
    {
        this.patronManager = patronManager;
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Patron Search[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Search by Name",
                        "2. Search by Membership Number",
                        "0. Go back"
                    }));

            switch (choice)
            {
                case "1. Search by Name":
                    SearchByName();
                    break;
                case "2. Search by Membership Number":
                    SearchByMemberShipNumber();
                    break;
                case "0. Go back":
                    return;
                default:
                    Handler.HandleError(new InvalidInputException("Invalid option. Please try again."));
                    break;
            }
        }
    }

    private void SearchByName()
    {
        string name = AnsiConsole.Ask<string>("Enter [yellow]name[/] to search:");
        if (!string.IsNullOrEmpty(name))
        {
            patronManager.ShowPatronByName(name);
        }
        else
        {
            Handler.HandleError(new InvalidInputException("Name cannot be empty."));
        }
        
    }

    private void SearchByMemberShipNumber()
    {
        string membershipNumber = AnsiConsole.Ask<string>("Enter [yellow]membership number[/] to search:");
        if (!string.IsNullOrEmpty(membershipNumber))
        {
            patronManager.ShowPatronByMembershipNumber(membershipNumber);
            AnsiConsole.MarkupLine("[green]Search completed.[/]");
        }
        else
        {
            Handler.HandleError(new InvalidInputException("Membership number cannot be empty."));
        }
        Pause();
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
