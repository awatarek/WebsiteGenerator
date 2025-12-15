using WebsiteGenerator.Services;

Console.WriteLine("=== GENERATOR STRONY INTERNETOWEJ ===\n");

var inputService = new UserInputService();
var config = inputService.GatherUserInput();

var directoryService = new DirectoryService();
var projectPath = directoryService.CreateProjectStructure(config);

var fileGenerator = new FileGeneratorService();
fileGenerator.GenerateAll(config, projectPath);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\n✓ Projekt wygenerowany pomyślnie");
Console.WriteLine($"Lokalizacja: {projectPath}");
Console.ResetColor();
