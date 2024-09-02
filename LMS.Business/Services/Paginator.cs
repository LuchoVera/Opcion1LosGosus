using Spectre.Console;

namespace LMS.Business.Services;

public static class Paginator
{
    public static void Paginate<T>(List<T> items, int pageSize = 5, Func<T, string>? displayFormatter = null)
    {
        int pageIndex = 0;

        int pageCount = (int)Math.Ceiling(items.Count / (double)pageSize);

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[bold yellow]List of {typeof(T).Name}[/]");

            var pageItems = items.GetRange(pageIndex * pageSize, Math.Min(pageSize, items.Count - pageIndex * pageSize));

            foreach (var item in pageItems)
            {
                string displayText = displayFormatter != null ? displayFormatter(item) : item?.ToString() ?? string.Empty;
                AnsiConsole.MarkupLine($"[green]{displayText}[/]");
            }

            Console.WriteLine();
            if (pageIndex > 0)
            {
                AnsiConsole.MarkupLine("[bold blue](P) Previous Page[/]");
            }
            if (pageIndex < pageCount - 1)
            {
                AnsiConsole.MarkupLine("[bold blue](N) Next Page[/]");
            }
            AnsiConsole.MarkupLine("[bold red](Q) Quit[/]");

            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.P && pageIndex > 0)
            {
                pageIndex--;
            }
            else if (key == ConsoleKey.N && pageIndex < pageCount - 1)
            {
                pageIndex++;
            }
            else if (key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}
