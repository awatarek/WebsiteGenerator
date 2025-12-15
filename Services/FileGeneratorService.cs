using System.Text;
using WebsiteGenerator.Models;
using WebsiteGenerator.Templates;

namespace WebsiteGenerator.Services;

public class FileGeneratorService
{
    public void GenerateAll(ProjectConfig config, string path)
    {
        File.WriteAllText(Path.Combine(path, "index.html"),
            HtmlTemplate.GenerateIndex(config), Encoding.UTF8);

        if (config.IncludeCss)
            File.WriteAllText(
                Path.Combine(path, "style.css"),
                CssTemplate.Generate(),
                Encoding.UTF8
            );


        if (config.IncludeJs)
            File.WriteAllText(
                Path.Combine(path, "script.js"),
                JsTemplate.Generate(),
                Encoding.UTF8
            );

        if (config.IncludeRobotsTxt)
            File.WriteAllText(Path.Combine(path, "robots.txt"),
                "User-agent: *\nDisallow:", Encoding.UTF8);

        if (config.AdditionalPages.Any())
        {
            var pagesDir = Path.Combine(path, "pages");
            Directory.CreateDirectory(pagesDir);

            foreach (var page in config.AdditionalPages)
            {
                var file = Normalize(page);
                File.WriteAllText(
                    Path.Combine(pagesDir, $"{file}.html"),
                    HtmlTemplate.GeneratePage(config, page),
                    Encoding.UTF8);
            }
        }

        GenerateReadme(config, path);
        GenerateSitemap(config, path);
    }

    private void GenerateReadme(ProjectConfig config, string path)
    {
        var content = $"""
        # {config.Title}

        Strona wygenerowana automatycznie.

        ## Uruchomienie
        Otwórz plik index.html w przeglądarce.
        """;

        File.WriteAllText(Path.Combine(path, "README.md"), content);
    }

    private void GenerateSitemap(ProjectConfig config, string path)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
        sb.AppendLine("<url><loc>index.html</loc></url>");

        foreach (var page in config.AdditionalPages)
        {
            var file = Normalize(page);
            sb.AppendLine($"<url><loc>pages/{file}.html</loc></url>");
        }

        sb.AppendLine("</urlset>");

        File.WriteAllText(Path.Combine(path, "sitemap.xml"), sb.ToString());
    }

    private string Normalize(string text) =>
        text.ToLower().Replace(" ", "-");
}
