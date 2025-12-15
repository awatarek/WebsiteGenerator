using WebsiteGenerator.Models;

namespace WebsiteGenerator.Services;

public class UserInputService
{
    public ProjectConfig GatherUserInput()
    {
        var config = new ProjectConfig();

        Console.Write("Podaj nazwę projektu: ");
        config.ProjectName = ReadRequired();

        Console.Write("Podaj tytuł strony: ");
        config.Title = ReadRequired();

        Console.Write("Podaj opis strony: ");
        config.Description = Console.ReadLine() ?? "";

        Console.Write("Podaj autora strony: ");
        config.Author = Console.ReadLine() ?? "";

        Console.Write("Podaj słowa kluczowe (oddzielone przecinkami): ");
        config.Keywords = Console.ReadLine() ?? "";

        Console.Write("Podaj język strony (np. pl, en): ");
        config.Language = Console.ReadLine() ?? "pl";

        config.IncludeCss = AskYesNo("Czy dodać plik CSS?");
        config.IncludeJs = AskYesNo("Czy dodać plik JavaScript?");
        config.IncludeFavicon = AskYesNo("Czy dodać favicon?");
        config.IncludeRobotsTxt = AskYesNo("Czy dodać robots.txt?");

        if (AskYesNo("Czy chcesz dodać podstrony?"))
        {
            Console.Write("Podaj nazwy podstron (oddzielone przecinkami): ");
            var input = Console.ReadLine() ?? "";
            config.AdditionalPages = input
                .Split(',')
                .Select(p => p.Trim())
                .Where(p => p.Length > 0)
                .ToList();
        }

        return config;
    }

    private bool AskYesNo(string question)
    {
        Console.Write($"{question} (t/n): ");
        var answer = Console.ReadLine()?.ToLower();
        return answer == "t" || answer == "tak";
    }

    private string ReadRequired()
    {
        while (true)
        {
            var value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            Console.Write("Pole nie może być puste. Spróbuj ponownie: ");
        }
    }
}
