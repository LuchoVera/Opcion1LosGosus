using Spectre.Console;

public class BorrowingUI
{
    private BorrowingManager _borrowingManager;
    private PatronManager _patronManager;
    private BookManager _bookManager;
    private ReserveManager _reserveManager;
    private PatronActions patronActions;

    public BorrowingUI(BorrowingManager borrowingManager, PatronManager patronManager, BookManager bookManager, ReserveManager reserveManager)
    {
        _borrowingManager = borrowingManager;
        _patronManager = patronManager;
        _bookManager = bookManager;
        _reserveManager = reserveManager;
        patronActions = new PatronActions(borrowingManager, patronManager, bookManager, reserveManager);
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Borrowing Options[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Borrow a Book",
                        "2. Return a Book",
                        "3. Print Borrowing History",
                        "4. Generate Reports",
                        "5. Generate Patron Report",
                        "0. Go back"
                    }));

            switch (choice)
            {
                case "1. Borrow a Book":
                    BorrowBook();
                    break;
                case "2. Return a Book":
                    ReturnBook();
                    break;
                case "3. Print Borrowing History":
                    PrintBorrowingHistory();
                    break;
                case "4. Generate Reports":
                    GenerateReports();
                    break;
                case "5. Generate Patron Report":
                    GeneratePatronReport();
                    break;
                case "0. Go back":
                    return;
                default:
                    ErrorHandler.HandleError(new InvalidInputException("Invalid option. Please try again."));
                    break;
            }
        }
    }

    private void BorrowBook()
    {
        string membershipNumber = AnsiConsole.Ask<string>("Enter [yellow]Patron MembershipNumber[/]:");
        string bookISBN = AnsiConsole.Ask<string>("Enter [yellow]Book ISBN[/]:");

        if (!string.IsNullOrEmpty(membershipNumber) && !string.IsNullOrEmpty(bookISBN))
        {
            patronActions.BorrowBook(membershipNumber, bookISBN);
        }
        else
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book ISBN or Patron MembershipNumber. Please try again."));
        }

        Pause();
    }

    private void ReturnBook()
    {
        string membershipNumber = AnsiConsole.Ask<string>("Enter [yellow]Patron MembershipNumber[/]:");
        string bookISBN = AnsiConsole.Ask<string>("Enter [yellow]Book ISBN[/]:");

        if (!string.IsNullOrEmpty(membershipNumber) && !string.IsNullOrEmpty(bookISBN))
        {
            patronActions.ReturnBook(membershipNumber, bookISBN);
        }
        else
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book ISBN or Patron MembershipNumber. Please try again."));
        }

        Pause();
    }

    private void PrintBorrowingHistory()
    {
        string membershipNumber = AnsiConsole.Ask<string>("Enter [yellow]Patron MembershipNumber[/]:");

        if (!string.IsNullOrEmpty(membershipNumber))
        {
            patronActions.PrintBorrowingHistory(membershipNumber);
        }
        else
        {
            ErrorHandler.HandleError(new InvalidPatronException("Invalid Patron MembershipNumber. Please try again."));
        }
    }

    private void GenerateReports()
    {
        try
        {
            string reportsDirectory = "Reports";

            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }

            var borrowedBooksReport = _bookManager.GetCurrentBorrowedBooks();
            File.WriteAllText(Path.Combine(reportsDirectory, "CurrentlyBorrowedBooks.txt"), borrowedBooksReport);
            AnsiConsole.MarkupLine("[green]Currently borrowed books report generated.[/]");

            var overdueBooksReport = _borrowingManager.GetOverdueBooks();
            File.WriteAllText(Path.Combine(reportsDirectory, "OverdueBooks.txt"), overdueBooksReport);
            AnsiConsole.MarkupLine("[green]Overdue books report generated.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]Error generating reports: {0}[/]");
            ErrorHandler.HandleError(ex);
        }
        Pause();
    }

    private void GeneratePatronReport()
    {
        try
        {
            string reportsDirectory = "Reports";

            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }

            string membershipNumber = AnsiConsole.Ask<string>("Enter [yellow]Patron MembershipNumber[/]:");

            if (!string.IsNullOrEmpty(membershipNumber))
            {
                var borrowingHistoryReport = _borrowingManager.GetBorrowingHistory(membershipNumber);
                File.WriteAllText(Path.Combine(reportsDirectory, $"{membershipNumber}_BorrowingHistory.txt"), borrowingHistoryReport);
            }
            else
            {
                ErrorHandler.HandleError(new InvalidPatronException("Invalid Patron MembershipNumber. Please try again."));
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]Error generating reports: {0}[/]");
            ErrorHandler.HandleError(ex);
        }
        Pause();
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
