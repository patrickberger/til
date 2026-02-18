namespace TilTool.Cli.Commands;

using System.Text;

using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;

using JetBrains.Annotations;

using TilTool.Cli.Core;

[Command("add", Description = "Adds a new til entry.")]
[UsedImplicitly]
public sealed class AddCommand : ICommand
{
    private const string DefaultTilFolder = "til";

    [CommandOption("category", 'c', Description = "The category of the til.", IsRequired = true)]
    public required string Category { get; init; }

    [CommandOption("root", 'r', Description = "The root directory of the til. Defaults to the directory above folder 'src'.", IsRequired = false)]
    public string RootFolder { get; init; } = @".\..\..\..";

    [CommandOption("title", 't', Description = "The title of the til.", IsRequired = false)]
    public string Title { get; init; } = "new til";

    public ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine($"Adding new til '{Title}' to category '{Category}'.");

        var categoryFolder = GetCategoryFolder();
        Directory.CreateDirectory(categoryFolder);
        console.Output.WriteLine($"Category folder: '{categoryFolder.Replace("\\", "/")}'.");
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        var fileName = GetFileName(today);
        var filePath = Path.Combine(categoryFolder, fileName);
        console.Output.WriteLine($"File name: '{fileName}'.");
        if (File.Exists(filePath)) throw new CommandException($"File '{filePath}' already exists.");

        var frontMatter = new FrontMatter { Title = Title, Date = today };
        var content = $"""
                       ---
                       {Yaml.Serialize(frontMatter)}
                       ---
                       
                       # {Title}
                       """;

        File.WriteAllText(filePath, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        console.Output.WriteLine($"Created file: {filePath.Replace("\\", "/")}.");
        return default;
    }

    private string GetCategoryFolder()
    {
        var rootFolder = Path.GetFullPath(RootFolder);
        var categorySlug = PathUtilities.Slugify(Category);
        return Path.Combine(rootFolder, DefaultTilFolder, categorySlug);
    }

    private string GetFileName(DateOnly date)
    {
        var titleSlug = PathUtilities.Slugify(Title);
        return $"{date:yyyy-MM-dd}-{titleSlug}.md";
    }
}
