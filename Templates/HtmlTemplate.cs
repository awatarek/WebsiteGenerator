using System.Globalization;
using System.Text;
using WebsiteGenerator.Models;

namespace WebsiteGenerator.Templates;

public static class HtmlTemplate
{
    public static string GenerateIndex(ProjectConfig config)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine($"<html lang=\"{config.Language}\">");
        sb.AppendLine("<head>");
        sb.AppendLine("  <meta charset=\"UTF-8\">");
        sb.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        sb.AppendLine($"  <title>{config.Title}</title>");
        sb.AppendLine($"  <meta name=\"description\" content=\"{config.Description}\">");
        sb.AppendLine($"  <meta name=\"author\" content=\"{config.Author}\">");
        sb.AppendLine($"  <meta name=\"keywords\" content=\"{config.Keywords}\">");

        if (config.IncludeCss)
            sb.AppendLine("  <link rel=\"stylesheet\" href=\"style.css\">");

        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine($"<h1>{config.Title}</h1>");
        sb.AppendLine(GenerateMenu(config));
        sb.AppendLine("<p>Strona wygenerowana automatycznie.</p>");

        if (config.IncludeJs)
            sb.AppendLine("<script src=\"script.js\"></script>");

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    public static string GeneratePage(ProjectConfig config, string pageName)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine($"<html lang=\"{config.Language}\">");
        sb.AppendLine("<head>");
        sb.AppendLine("  <meta charset=\"UTF-8\">");
        sb.AppendLine($"  <title>{pageName} - {config.Title}</title>");

        if (config.IncludeCss)
            sb.AppendLine("  <link rel=\"stylesheet\" href=\"../style.css\">");

        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine(GenerateMenu(config, true));
        sb.AppendLine($"<h1>{pageName}</h1>");
        sb.AppendLine("<p>Podstrona wygenerowana automatycznie.</p>");
        sb.AppendLine("<a href=\"../index.html\">Powrót</a>");

        if (config.IncludeJs)
            sb.AppendLine("<script src=\"../script.js\"></script>");

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    private static string GenerateMenu(ProjectConfig config, bool isSubPage = false)
    {
        if (!config.AdditionalPages.Any())
            return "";

        var sb = new StringBuilder();
        var prefix = isSubPage ? "../" : "";

        sb.AppendLine("<nav><ul>");
        sb.AppendLine($"<li><a href=\"{prefix}index.html\">Strona główna</a></li>");

        foreach (var page in config.AdditionalPages)
        {
            var file = Normalize(page);
            sb.AppendLine($"<li><a href=\"{prefix}pages/{file}.html\">{page}</a></li>");
        }

        sb.AppendLine("</ul></nav>");
        return sb.ToString();
    }

    private static string Normalize(string text)
    {
        var normalized = text.ToLower().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalized)
            if (Char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);

        return sb.ToString()
            .Normalize(NormalizationForm.FormC)
            .Replace(" ", "-");
    }
}
