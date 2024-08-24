using Spectre.Console;

public class ReserveUI
{
    private readonly ReserveManager _reserveManager;
    private readonly PatronManager _patronManager;
    private readonly BookManager _bookManager;
    private readonly PatronActions _patronActions;
    private BorrowingManager _borrowingManager;

    public ReserveUI(BorrowingManager borrowingManager,ReserveManager reserveManager, PatronManager patronManager, BookManager bookManager)
    {
        _borrowingManager = borrowingManager;
        _reserveManager = reserveManager;
        _patronManager = patronManager;
        _bookManager = bookManager;
        _patronActions = new PatronActions(borrowingManager, patronManager, bookManager, reserveManager);
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Reserve Options[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Reserve a Book",
                        "2. Check if a Book is Reserved",
                        "3. Go back"
                    }));

            switch (choice)
            {
                case "1. Reserve a Book":
                    ReserveBook();
                    break;
                case "2. Check if a Book is Reserved":
                    CheckBookReservation();
                    break;
                case "3. Go back":
                    return;
                default:
                    ErrorHandler.HandleError(new InvalidInputException("Invalid option. Please try again."));
                    break;
            }
        }
    }
    private void ReserveBook()
    {
        string membershipNumber = AnsiConsole.Ask<string>("Enter [yellow]Membership Number[/]:");
        string bookISBN = AnsiConsole.Ask<string>("Enter [yellow]Book ISBN[/]:");
        if (!string.IsNullOrEmpty(membershipNumber) && !string.IsNullOrEmpty(bookISBN))
        {
            Book? book = _bookManager.SearchBook(book => book.ISBN.Equals(bookISBN, StringComparison.OrdinalIgnoreCase));
            if (book != null)
            {
                if (!book.IsBorrowed)
                {
                    AnsiConsole.MarkupLine("[red]The book is not borrowed, so it cannot be reserved.[/]");
                }
                else
                {
                    _patronActions.ReserveBook(membershipNumber, bookISBN);
                    AnsiConsole.MarkupLine("[green]Book reserved successfully![/]");
                }
            }
            else
            {
                ErrorHandler.HandleError(new InvalidInputException("Invalid book ISBN. Please try again"));
            }
        }
        else
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book ISBN or Membership Number. Please try again"));
        }
        Pause();
    }

    private void CheckBookReservation()
    {
        string bookISBN = AnsiConsole.Ask<string>("Enter [yellow]Book ISBN[/]:");

        if (!string.IsNullOrEmpty(bookISBN))
        {
            Book? book = _bookManager.SearchBook(book => book.ISBN.Equals(bookISBN, StringComparison.OrdinalIgnoreCase));
            if (book != null && book.IsReserved)
            {
                AnsiConsole.MarkupLine("[green]The book is reserved.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]The book is not reserved.[/]");
            }
        }
        else
        {
            ErrorHandler.HandleError(new InvalidInputException("Invalid book ISBN. Please try again"));


        }

        Pause();
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
