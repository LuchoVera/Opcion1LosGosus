using LMS.Application.ConsoleInterface.ManagersUI;
using LMS.Application.ConsoleInterface.ServicesUI;
using LMS.Application.Controllers;
using LMS.Business.Managers;
using LMS.Business.Services;

using Spectre.Console;

namespace LMS.Application.ConsoleInterface;

public class MainMenuUI
{
    private static BookController bookController = new BookController();
    private static PatronController patronController = new PatronController();
    private static BorrowingManager borrowingManager = new BorrowingManager();
    private static ReserveManager reserveManager = new();
    private static LibraryStatistics libraryStatistics = new LibraryStatistics(bookController.GetBookManager(), borrowingManager, patronController.GetPatronManager());
    private BookManagerUI bookManagerUI = new BookManagerUI(bookController);
    private PatronManagerUI patronManagerUI = new PatronManagerUI(patronController);
    private BorrowingUI borrowingUI = new BorrowingUI(borrowingManager, patronController.GetPatronManager(), bookController.GetBookManager(), reserveManager);
    private StatisticsUI statisticsUI = new StatisticsUI(libraryStatistics);

    private ReserveUI reserveUI = new ReserveUI(borrowingManager, reserveManager, patronController.GetPatronManager(), bookController.GetBookManager());

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Main Menu[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Book Management",
                        "2. Patron Management",
                        "3. Borrowing Management",
                        "4. Reservation Management", 
                        "5. Statistics",
                        "0. Exit"
                    }));

            switch (choice)
            {
                case "1. Book Management":
                    bookManagerUI.Run();
                    break;
                case "2. Patron Management":
                    patronManagerUI.Run();
                    break;
                case "3. Borrowing Management":
                    borrowingUI.Run();
                    break;
                case "4. Reservation Management":  
                    reserveUI.Run();
                    break;
                case "5. Statistics":
                    statisticsUI.Run();
                    break;
                case "0. Exit":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    Pause();
                    break;
            }
        }
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
