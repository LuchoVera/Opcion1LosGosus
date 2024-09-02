using LMS.Business.Managers;
using LMS.Business.Services.ErrorHandler;
using LMS.Business.Services.ErrorHandler.Exceptions;

using Spectre.Console;

namespace LMS.Application.ConsoleInterface.ServicesUI;

public class BookSearcherUI
{
    private BookManager bookManager;

    public BookSearcherUI(BookManager bookManager)
    {
        this.bookManager = bookManager;
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Book Search[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Search by Title",
                        "2. Search by Author",
                        "3. Search by ISBN",
                        "0. Go back"
                    }));

            switch (choice)
            {
                case "1. Search by Title":
                    SearchByTitle();
                    break;
                case "2. Search by Author":
                    SearchByAuthor();
                    break;
                case "3. Search by ISBN":
                    SearchByISBN();
                    break;
                case "0. Go back":
                    return;
                default:
                    Handler.HandleError(new InvalidInputException("Invalid option. Please try again."));
                    break;
            }
        }
    }

    private void SearchByTitle()
    {
        string title = AnsiConsole.Ask<string>("Enter [yellow]title[/] to search:");
        if (!string.IsNullOrEmpty(title))
        {
            bookManager.ShowBookByTitle(title);
        }
        else
        {
            Handler.HandleError(new InvalidInputException("Title cannot be empty."));
        }
        
    }

    private void SearchByAuthor()
    {
        string author = AnsiConsole.Ask<string>("Enter [yellow]author[/] to search:");
        if (!string.IsNullOrEmpty(author))
        {
            bookManager.ShowBookByAuthor(author);
        }
        else
        {
            Handler.HandleError(new InvalidInputException("Author cannot be empty."));
        }
        
    }

    private void SearchByISBN()
    {
        string isbn = AnsiConsole.Ask<string>("Enter [yellow]ISBN[/] to search:");
        if (!string.IsNullOrEmpty(isbn))
        {
            bookManager.ShowBookByISBN(isbn);
        }
        else
        {
            Handler.HandleError(new InvalidInputException("ISBN cannot be empty."));
        }
        Pause();
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
