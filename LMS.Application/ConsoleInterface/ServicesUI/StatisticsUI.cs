using LMS.Business.Services;

using Spectre.Console;

namespace LMS.Application.ConsoleInterface.ServicesUI;

public class StatisticsUI
{
    private LibraryStatistics libraryStatistics;

    public StatisticsUI(LibraryStatistics libraryStatistics)
    {
        this.libraryStatistics = libraryStatistics;
    }

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Library Management System - Statistics[/]")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Most Borrowed Books",
                        "2. Most Active Patrons",
                        "3. Books Quantity by Genre",
                        "4. Go back"
                    }));

            switch (choice)
            {
                case "1. Most Borrowed Books":
                    ShowMostBorrowedBooks();
                    break;
                case "2. Most Active Patrons":
                    ShowMostActivePatrons();
                    break;
                case "3. Books Quantity by Genre":
                    ShowBooksByGenre();
                    break;
                case "4. Go back":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Invalid option. Please try again.[/]");
                    break;
            }
        }
    }

    private void ShowMostBorrowedBooks()
    {
        var mostBorrowedBooks = libraryStatistics.GetMostBorrowedBooks();

        if (mostBorrowedBooks.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No records available for most borrowed books.[/]");
        }
        else
        {
            var bookChart = new BarChart()
                .Width(60)
                .Label("[green bold]Most Borrowed Books[/]")
                .CenterLabel();

            foreach (var book in mostBorrowedBooks)
            {
                bookChart.AddItem(book.Title, book.BorrowCount, GenerateColor());
            }

            AnsiConsole.Write(bookChart);
        }

        Pause();
    }

    private void ShowMostActivePatrons()
    {
        var mostActivePatrons = libraryStatistics.GetMostActivePatrons();

        if (mostActivePatrons.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No records available for most active patrons.[/]");
        }
        else
        {
            var patronChart = new BarChart()
                .Width(60)
                .Label("[yellow]Most Active Patrons[/]")
                .CenterLabel();

            foreach (var patron in mostActivePatrons)
            {
                patronChart.AddItem(patron.PatronName, patron.BorrowCount, GenerateColor());
            }

            AnsiConsole.Write(patronChart);
        }

        Pause();
    }

    private void ShowBooksByGenre()
    {
        var booksByGenre = libraryStatistics.GetBooksByGenre();

        if (booksByGenre.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No records available for books by genre.[/]");
        }
        else
        {
            var genreChart = new BarChart()
                .Width(60)
                .Label("[yellow]Books by Genre[/]")
                .CenterLabel();

            foreach (var genre in booksByGenre)
            {
                genreChart.AddItem(genre.Genre, genre.Count, GenerateColor());
            }

            AnsiConsole.Write(genreChart);
        }

        Pause();
    }

    private Color GenerateColor()
    {
        var random = new Random();
        var color = new Color(
            (byte)random.Next(256),
            (byte)random.Next(256),
            (byte)random.Next(256));
        return color;
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
        Console.ReadKey(true);
    }
}
