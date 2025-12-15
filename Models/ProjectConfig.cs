namespace WebsiteGenerator.Models;

public class ProjectConfig
{
    public string ProjectName { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Author { get; set; } = "";
    public string Keywords { get; set; } = "";
    public string Language { get; set; } = "pl";

    public bool IncludeCss { get; set; }
    public bool IncludeJs { get; set; }
    public bool IncludeFavicon { get; set; }
    public bool IncludeRobotsTxt { get; set; }

    public List<string> AdditionalPages { get; set; } = new();
}
