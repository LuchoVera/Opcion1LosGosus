using Spectre.Console;

public class MainMenuUI
{
    private static BookController bookController = new BookController();
    private static PatronController patronController = new PatronController();
    private static BorrowingManager borrowingManager = new BorrowingManager();
    private BookManagerUI bookManagerUI = new BookManagerUI(bookController);
    private PatronManagerUI patronManagerUI = new PatronManagerUI(patronController);
    private BorrowingUI borrowingUI = new BorrowingUI(borrowingManager, patronController.GetPatronManager(), bookController.GetBookManager());

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
                        "4. Exit"
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
                case "4. Exit":
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
