using WebsiteGenerator.Models;

namespace WebsiteGenerator.Services;

public class DirectoryService
{
    public string CreateProjectStructure(ProjectConfig config)
    {
        var projectPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            config.ProjectName
        );

        // Folder główny projektu
        Directory.CreateDirectory(projectPath);

        // Zawsze tworzymy images
        Directory.CreateDirectory(Path.Combine(projectPath, "images"));

        // Opcjonalne foldery
        if (config.AdditionalPages.Any())
        {
            Directory.CreateDirectory(Path.Combine(projectPath, "pages"));
        }

        if (config.IncludeCss || config.IncludeJs)
        {
            Directory.CreateDirectory(Path.Combine(projectPath, "fonts"));
        }

        return projectPath;
    }
}
